using Ethan.FMS.Application.Exceptions;
using Ethan.FMS.Application.Services.FileSystemService;
using Ethan.FMS.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FileNotFoundException = Ethan.FMS.Application.Exceptions.FileNotFoundException;

namespace Ethan.FMS.Application.Files.Commands.Delete;

public class DeleteFileCommand:IRequest<string>
{
    public int Id { get; set; }
}
public class DeleteFileCommandHandler:IRequestHandler<DeleteFileCommand,string>
{
    private readonly EthanFmsDbContext _context;
    private readonly IFileSystemService _fileSystemService;

    public DeleteFileCommandHandler(EthanFmsDbContext context, IFileSystemService fileSystemService)
    {
        _context = context;
        _fileSystemService = fileSystemService;
    }

    public  async Task<string> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        var file = await _context.Files.FirstOrDefaultAsync(file =>  file.Id== request.Id, cancellationToken);
        if (file == null) throw new FileNotFoundException();
        await _fileSystemService.DeleteFile(file.Path);
        _context.Remove(file);
        await _context.SaveChangesAsync(cancellationToken);
        return "File successfully deleted.";
    }
}