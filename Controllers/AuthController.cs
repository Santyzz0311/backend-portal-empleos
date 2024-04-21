using backend_portal_empleos.Data;
using backend_portal_empleos.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_portal_empleos.Services;
using Microsoft.AspNetCore.Authorization;


namespace backend_portal_empleos.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly JwtTokenService _jwtTokenService;
        private readonly ILogger<JwtTokenService> _logger;

        public AuthController(DataContext context, JwtTokenService jwtTokenService, ILogger<JwtTokenService> logger)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.Password == loginDto.Password);


            if (user == null)
            {
                return Unauthorized("Credenciales inválidas");
            }

            var token = _jwtTokenService.GenerateToken(user.Email, user.Id, user.Name);
            return Ok(new
            {
                Token = token,
                User = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    RoleName = user.Rol != null ? user.Rol.Name : string.Empty,
                }
            });
        }

        [HttpGet("validate_token")]
        [Authorize] // Asegura que este endpoint requiera autenticación JWT
        public IActionResult ValidateToken()
        {
            _logger.LogInformation("Validating token");

            // Log each claim received in the token
            foreach (var claim in User.Claims)
            {
                _logger.LogInformation("Claim received - Type: {ClaimType}, Value: {ClaimValue}", claim.Type, claim.Value);
            }

            // Intenta extraer el ID del usuario y el email de los claims
            var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            var email = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
            var name = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name))
            {
                return Unauthorized("Invalid token"); // Si no se encuentran los datos necesarios, retorna un error
            }

            return Ok(new UserDto
            {
                Id = int.Parse(userId),
                Name = name,
                Email = email
            });
        }
    }
}
