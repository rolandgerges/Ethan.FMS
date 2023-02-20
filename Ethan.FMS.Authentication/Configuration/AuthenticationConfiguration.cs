using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Ethan.FMS.Authentication.Configuration;

public static class AuthenticationConfiguration
{
    public static void AddAuthenticationServices(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "Jwt:Issuer",
                    ValidAudience = "Jwt:Audience",
                    IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAjEbkFazjq3x7IxO9O41GZVQDa2ZzX0fiW2WLYPbldAXAQCbShv6sCkGpR01DWQ7muPk7j5dEr2ew1tZS9CutCv6C5vOdMcwjBw+3r6pBEcYsRgEF7PfJOejR08K1EwneLh8keOEpehNCaA37a5CmPbTp1JpujWgO08uBU8F5apPO+cyQUuAY4tg8sLowZyXW649B/Wwapi2e4akx3DOGElOn7qO2uZPs8K+gQRmUtsGIYtC2nJ3U4/5qfh+vwUH5ET78mISwG+Nsqf2RM17qqKqXqOJDeJdqs7o4VoZyFyYWyTAr5lttR++mawE727McjM/2uAmCBfCbXIKmvKKLpwIDAQAB")),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy("CreateFiles", policy => policy.RequireClaim("scope", "create:files").Build());
            options.AddPolicy("ReadFiles", policy => policy.RequireClaim("scope", "read:files").Build());
            options.AddPolicy("DeleteFiles", policy => policy.RequireClaim("scope", "delete:files").Build());
        });
    }

}


