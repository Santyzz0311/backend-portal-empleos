using Microsoft.AspNetCore.Mvc;
using backend_portal_empleos.Data;
using backend_portal_empleos.Entities;
using backend_portal_empleos.DTOs;
using Microsoft.EntityFrameworkCore;

[Route("jobs")]
[ApiController]
public class JobsController : ControllerBase
{
    private readonly DataContext _context;

    public JobsController(DataContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Jobs>> GetJob(int id)
    {
        var job = await _context.Jobs
            .Include(j => j.User) 
            .Include(j => j.Category)
            .FirstOrDefaultAsync(j => j.Id == id);

        if (job == null)
        {
            return NotFound();
        }

        return Ok(job);
    }


    [HttpPost]
    public async Task<ActionResult<Jobs>> CreateJob([FromBody] JobCreateDto jobDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Verifica si el usuario y la categoría existen
        var userExists = await _context.Users.AnyAsync(u => u.Id == jobDto.UserId);
        var categoryExists = await _context.Categories.AnyAsync(c => c.Id == jobDto.CategoryId);

        if (!userExists || !categoryExists)
        {
            return BadRequest("UserId o CategoryId Invalido");
        }

        var job = new Jobs
        {
            Title = jobDto.Title,
            Description = jobDto.Description,
            Location = jobDto.Location,
            UserId = jobDto.UserId,
            CategoryId = jobDto.CategoryId,
            PublicationDate = DateTime.Now,
            IsOpen = true
        };

        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetJob", new { id = job.Id }, job);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobGetDto>>> GetAllJobsToApply(int userId)
    {
        // Recuperar todas las aplicaciones del usuario para saber a qué trabajos ha aplicado
        var userAppliedJobIds = await _context.Applications
            .Where(a => a.UserId == userId)
            .Select(a => a.JobId)
            .Distinct()
            .ToListAsync();

        // Consulta todos los trabajos y verifica si el usuario ha aplicado a cada uno
        var jobs = await _context.Jobs
            .Include(job => job.User)
            .Include(job => job.Category)
            .Select(job => new JobGetDto
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                Location = job.Location,
                CreatorUserName = job.User != null ? job.User.Name : string.Empty,
                CreatorEmail = job.User != null ? job.User.Email : string.Empty,
                CategoryName = job.Category != null ? job.Category.Name : string.Empty,
                HasApplied = userAppliedJobIds.Contains(job.Id)
            })
            .ToListAsync();

        return Ok(jobs);
    }

    [HttpGet("created/{creatorId}")]
    public async Task<ActionResult<IEnumerable<JobGetDto>>> GetAllCreatedJobs(int creatorId)
    {
        // Consulta todos los trabajos creados por el usuario especificado
        var jobs = await _context.Jobs
            .Where(job => job.UserId == creatorId)
            .Include(job => job.User)
            .Include(job => job.Category)
            .Select(job => new JobCreatedDto
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                Location = job.Location,
                CreatorUserName = job.User != null ? job.User.Name : string.Empty,
                CreatorEmail = job.User != null ? job.User.Email : string.Empty,
                CategoryName = job.Category != null ? job.Category.Name : string.Empty
            })
            .ToListAsync();

        if (jobs == null || jobs.Count == 0)
        {
            return NotFound("No hay trabajos creados por el usuario especificado");
        }

        return Ok(jobs);
    }



}
