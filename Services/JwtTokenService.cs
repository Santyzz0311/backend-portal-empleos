using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace backend_portal_empleos.Services;
public class JwtTokenService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<JwtTokenService> _logger;

    public JwtTokenService(IConfiguration configuration, ILogger<JwtTokenService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public string GenerateToken(string email, int userId, string name)
    {
        var key = _configuration["Jwt:Key"];
        if (string.IsNullOrEmpty(key))
        {
            throw new InvalidOperationException("JWT Key is not set in the configuration.");
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("userId", userId.ToString()),
            new Claim("name", name)
        };

        foreach (var claim in claims)
        {
            _logger.LogInformation("Adding claim {ClaimType}: {ClaimValue}", claim.Type, claim.Value);
        }

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            //expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
