using Ethan.FMS.Application.Services.TokenService;
using MediatR;

namespace Ethan.FMS.Application.Authentication.GetAuthenticationToken;

public class GetAuthenticationTokenQuery:IRequest<string>
{
    public string Type { get; set; }
}
public class GetAuthenticationTokenQueryHandler:IRequestHandler<GetAuthenticationTokenQuery,string>
{
    private readonly ITokenService _tokenService;

    public GetAuthenticationTokenQueryHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public Task<string> Handle(GetAuthenticationTokenQuery request, CancellationToken cancellationToken)
    {
        return  _tokenService.GenerateToken(request.Type);
    }
}