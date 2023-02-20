using Ethan.FMS.Application.Exceptions;
using Ethan.FMS.Application.Services.FileSystemService;
using Ethan.FMS.Persistence.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ethan.FMS.Application.Files.Commands.Add;

public class AddFileCommand : IRequest<long>
{
    public IFormFile File { get; set; }
}

public class AddFileCommandHandler : IRequestHandler<AddFileCommand, long>
{
    private readonly EthanFmsDbContext _context;
    private readonly IFileSystemService _fileSystemService;

    public AddFileCommandHandler(EthanFmsDbContext context, IFileSystemService fileSystemService)
    {
        _context = context;
        _fileSystemService = fileSystemService;
    }

    public async Task<long> Handle(AddFileCommand request, CancellationToken cancellationToken)
    {
        if (request.File.Length == 0)
        {
            throw new EmptyFileException();
        }

        var file = new Persistence.Models.Files()
        {
            Name = request.File.FileName,
            Size = request.File.Length,
            CreatedAt = DateTime.Now,
            Path = Path.Combine("Files", request.File.FileName)
        };
        var saved = false;
        var counter = 1;
        do
        {
            try
            {
                await _fileSystemService.SaveFileAsync("Files", file.Name, request.File);
                saved = true;
            }
            catch (FileAlreadyExistsException)
            {
                var lastDotIndex = request.File.FileName.LastIndexOf('.');
                file.Name = request.File.FileName.Substring(0, lastDotIndex) + $"({counter})" +
                            request.File.FileName.Substring(lastDotIndex);
                file.Path= Path.Combine("Files", file.Name);
            }

            counter++;
        } while (!saved);


        var savedFile = _context.Files.Add(file).Entity;
        await _context.SaveChangesAsync(cancellationToken);

        return savedFile.Id;
    }
}