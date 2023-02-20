using Ethan.FMS.Application.Authentication.GetAuthenticationToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ethan.FMS.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class AuthenticationController:ControllerBase
{
    private readonly IMediator _mediator;
    public AuthenticationController(IMediator mediator) =>
        _mediator = mediator;
    
    // hardcoded for testing purposes
    [HttpGet("ReadToken")]
    public async Task<ActionResult> GetReadToken()
    {
        return Ok( await _mediator.Send( new GetAuthenticationTokenQuery()
        {
            Type = "read:files"
        }));
    }
    [HttpGet("WriteToken")]
    public async Task<ActionResult> GetWriteToken()
    {
        return Ok( await _mediator.Send( new GetAuthenticationTokenQuery()
        {
            Type = "create:files"
        }));
    }
    [HttpGet("DeleteToken")]
    public async Task<ActionResult> GetDeleteToken()
    {
        return Ok( await _mediator.Send( new GetAuthenticationTokenQuery()
        {
            Type =  "delete:files"
        }));
    }
}