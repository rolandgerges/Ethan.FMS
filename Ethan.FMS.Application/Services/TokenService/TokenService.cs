using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Ethan.FMS.Application.Services.TokenService;

public class TokenService:ITokenService

{
    public async Task<string> GenerateToken(string type)
    {
       
        var issuer = "Jwt:Issuer";
        var audience = "Jwt:Audience";
        var key = Encoding.ASCII.GetBytes
            ("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAjEbkFazjq3x7IxO9O41GZVQDa2ZzX0fiW2WLYPbldAXAQCbShv6sCkGpR01DWQ7muPk7j5dEr2ew1tZS9CutCv6C5vOdMcwjBw+3r6pBEcYsRgEF7PfJOejR08K1EwneLh8keOEpehNCaA37a5CmPbTp1JpujWgO08uBU8F5apPO+cyQUuAY4tg8sLowZyXW649B/Wwapi2e4akx3DOGElOn7qO2uZPs8K+gQRmUtsGIYtC2nJ3U4/5qfh+vwUH5ET78mISwG+Nsqf2RM17qqKqXqOJDeJdqs7o4VoZyFyYWyTAr5lttR++mawE727McjM/2uAmCBfCbXIKmvKKLpwIDAQAB");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim("scope", type),
                //new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                // new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(5),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        var stringToken = tokenHandler.WriteToken(token);
        return stringToken;
    }
}