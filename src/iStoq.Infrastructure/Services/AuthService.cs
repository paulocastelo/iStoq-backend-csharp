using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using iStoq.Application.DTOs;
using iStoq.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace iStoq.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;

    public AuthService(IConfiguration config)
    {
        _config = config;
    }

    public LoginResponseDto? Authenticate(LoginRequestDto dto)
    {
        if (dto.Username != "admin" || dto.Password != "123456")
            return null;

        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
        var issuer = _config["Jwt:Issuer"];

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, dto.Username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = issuer,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(tokenDescriptor);

        return new LoginResponseDto
        {
            Token = handler.WriteToken(token),
            ExpiresAt = tokenDescriptor.Expires!.Value
        };
    }
}
