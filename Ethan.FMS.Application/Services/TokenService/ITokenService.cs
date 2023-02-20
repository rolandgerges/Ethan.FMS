
namespace Ethan.FMS.Application.Services.TokenService;

public interface ITokenService

{
     Task<string> GenerateToken(string type);
     
     
}