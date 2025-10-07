using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectsComposer.BuisnessLogic;
using ProjectsComposer.Core.Models;

namespace ProjectsComposer.API.Services;

public class JwtService(IOptions<AuthSettings> options)
{
    public string GenerateJwtToken(Account account)
    {
        var claims = new List<Claim>
        {
            new("userName", account.UserName),
            new("id", account.Id.ToString())
        };

        var jwtToken = new JwtSecurityToken(
            expires: DateTime.UtcNow.Add(options.Value.Expires),
            claims: claims,
            signingCredentials: 
            new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(options.Value.SecretKey)),
                        SecurityAlgorithms.HmacSha256)); // TODO: FIND OUT THE DIFFERENCES
        
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}