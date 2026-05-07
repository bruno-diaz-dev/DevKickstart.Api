using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DevKickstart.Api.Services.Auth;

public class TokenService
{
    private const string SecretKey = "ESTA_ES_UNA_SECRET_KEY_SUPER_LARGA";

    public string CrearToken(string username)
    {
        var TokenHandler = new JwtSecurityTokenHandler();
        var Key = Encoding.UTF8.GetBytes(SecretKey);
        var claims= new[]
        {
            new Claim(ClaimTypes.Name, username)
        };
        var descriptor = new SecurityTokenDescriptor
        {
            Subject= new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = TokenHandler.CreateToken(descriptor);
        return TokenHandler.WriteToken(token);
    }
}