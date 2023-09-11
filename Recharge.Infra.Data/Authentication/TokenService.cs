using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Recharge.Domain.Models.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Recharge.Infra.Data.Authentication;
public class TokenService {

    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration) {
        _configuration = configuration;
    }

    public string GenerateToken(User user) {

        var claims = new List<Claim> {
        new Claim("Id", user.Id.ToString())
    };

        var secretKey = _configuration["JwtSettings:SecretKey"];

        var expires = DateTime.Now.AddHours(12);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var tokenData = new JwtSecurityToken(
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            expires: expires,
            claims: claims);

        var token = new JwtSecurityTokenHandler().WriteToken(tokenData);

        return token;
    }
}