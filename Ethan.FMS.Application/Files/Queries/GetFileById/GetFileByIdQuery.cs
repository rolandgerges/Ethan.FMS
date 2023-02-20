using Ethan.FMS.Application.Exceptions;
using Ethan.FMS.Application.Services.FileSystemService;
using Ethan.FMS.Application.ViewModels;
using Ethan.FMS.Persistence.Models;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using FileNotFoundException = Ethan.FMS.Application.Exceptions.FileNotFoundException;


namespace Ethan.FMS.Application.Files.Queries.GetFileById;

public class GetFileByIdQuery:IRequest<FileViewModel>
{
    public int Id { get; set; }
}
public class GetFilesQueryHandler:IRequestHandler<GetFileByIdQuery,FileViewModel>
{
    private readonly EthanFmsDbContext _context;
    private readonly IFileSystemService _fileSystemService;
    private readonly IHostingEnvironment _environment;
    public GetFilesQueryHandler(EthanFmsDbContext context, IFileSystemService fileSystemService, IHostingEnvironment environment)
    {
        _context = context;
        _fileSystemService = fileSystemService;
        _environment = environment;
    }

    public  async Task<FileViewModel> Handle(GetFileByIdQuery request, CancellationToken cancellationToken)
    {
        
        var file =  await _context.Files.FirstOrDefaultAsync(f => f.Id == request.Id,cancellationToken);
        if (file == null) throw new FileNotFoundException();
        if (file.Path == null) throw new NotLoadedFileException(file.Id);
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.Path);
        var fileBytes =  await File.ReadAllBytesAsync(filePath, cancellationToken);
        return new FileViewModel()
        {
            FileName = file.Name,
            FileBytes = fileBytes
        };

    }
}