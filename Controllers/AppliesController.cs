using Microsoft.AspNetCore.Mvc;
using backend_portal_empleos.Data;
using backend_portal_empleos.Entities;
using backend_portal_empleos.DTOs;
using Microsoft.EntityFrameworkCore;

[Route("applies")]
[ApiController]
public class AppliesController : ControllerBase
{
    private readonly DataContext _context;

    public AppliesController(DataContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Applications>> ApplyToJob([FromBody] ApplyToJobDto applyDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Revisa si el trabajo existe
        var jobExists = await _context.Jobs.AnyAsync(j => j.Id == applyDto.JobId);
        if (!jobExists)
        {
            return NotFound("Job not found.");
        }

        // Revisa si el usuario existe
        var userExists = await _context.Users.AnyAsync(u => u.Id == applyDto.UserId);
        if (!userExists)
        {
            return NotFound("User not found.");
        }

        // Revisa si el usuario ya aplico a este trabajo
        var alreadyApplied = await _context.Applications
            .AnyAsync(a => a.UserId == applyDto.UserId && a.JobId == applyDto.JobId);
        if (alreadyApplied)
        {
            return BadRequest("User has already applied to this job.");
        }

        var application = new Applications
        {
            UserId = applyDto.UserId,
            JobId = applyDto.JobId,
            ApplyDate = DateTime.Now,
            Description = applyDto.Description,
        };

        _context.Applications.Add(application);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetApplicationsByUserId", new { id = application.Id }, application);
    }

    // GET: api/applies/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<ActionResult> GetApplicationsByUserId(int userId)
    {
        var applications = await _context.Applications
            .Include(a => a.Job)
                .ThenInclude(job => job.User) 
            .Where(a => a.UserId == userId)
            .Select(a => new ApplyGetDto
            {
                JobId = a.JobId,
                JobTitle = a.Job != null ? a.Job.Title : string.Empty,
                AppliedOn = a.ApplyDate,
                ApplyDescription = a.Job != null ? a.Job.Description : string.Empty,
                JobLocation = a.Job != null ? a.Job.Location : string.Empty,
                JobCreatorName = a.Job.User != null ? a.Job.User.Name : string.Empty,
                JobCreatorEmail = a.Job.User != null ? a.Job.User.Email : string.Empty
            })
            .ToListAsync();

        if (applications == null || applications.Count == 0)
        {
            return NotFound("No applications found for the specified user.");
        }

        return Ok(applications);
    }

    [HttpGet("job/{jobId}")]
    public async Task<ActionResult<IEnumerable<ApplyUserInfoDto>>> GetApplicationsByJobId(int jobId)
    {
        var applications = await _context.Applications
            .Include(a => a.User)
            .Where(a => a.JobId == jobId)
            .Select(a => new ApplyUserInfoDto
            {
                ApplicationId = a.Id,
                UserId = a.UserId,
                UserName = a.User != null ? a.User.Name : string.Empty,
                UserEmail = a.User != null ? a.User.Email : string.Empty,
                ApplyDate = a.ApplyDate,
                Description = a.Description
            })
            .ToListAsync();

        if (applications == null || applications.Count == 0)
        {
            return NotFound("No se encontraron postulantes para el trabajo proporcionado");
        }

        return Ok(applications);
    }
}
