using Ethan.FMS.Application.Files.Commands.Add;
using Ethan.FMS.Application.Files.Commands.Delete;
using Ethan.FMS.Application.Files.Queries.GetFileById;
using Ethan.FMS.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ethan.FMS.WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class FileController:ControllerBase
{
    private readonly IMediator _mediator;
    public FileController(IMediator mediator) =>
        _mediator = mediator;
    

    [HttpPost]
    [Authorize(Policy = "CreateFiles")]
    [Route("AddFile()")]
    public async Task<IActionResult> AddFile([FromForm] AddFileCommand command)
    {
        return Ok(await _mediator.Send(command
        ));
    }
    
    [Authorize(Policy = "ReadFiles")]
    [HttpGet]
    public async Task<ActionResult> GetFileById([FromQuery] int id)
    {
        FileViewModel file = await _mediator.Send(new GetFileByIdQuery()
        {
            Id = id
        });
        return File(file.FileBytes, "application/octet-stream", file.FileName);
        
    }
    [Authorize(Policy = "DeleteFiles")]
    [HttpDelete]
    public async Task<ActionResult> DeleteFile([FromQuery] int id)
    {
        return Ok(await _mediator.Send(new DeleteFileCommand()
        {
            Id = id
        }));
    }

   
}