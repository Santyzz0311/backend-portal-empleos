using Microsoft.AspNetCore.Mvc;
using backend_portal_empleos.Data;
using Microsoft.EntityFrameworkCore;
using backend_portal_empleos.DTOs;

namespace backend_portal_empleos.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _context.Users
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                })
                .ToListAsync();

            return Ok(users);
        }
    }
}
