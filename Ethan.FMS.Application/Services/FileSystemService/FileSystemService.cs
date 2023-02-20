using Ethan.FMS.Application.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Ethan.FMS.Application.Services.FileSystemService;

public class FileSystemService : IFileSystemService
{
    private readonly IHostingEnvironment _env;

    public FileSystemService(IHostingEnvironment env)
    {
        _env = env;
    }

    public string GetRootPath()
    {
        return Path.Combine(_env.ContentRootPath, "wwwroot");
    }

    private string CreateDirectory(string path, string directory, string filename)
    {
        return Path.Combine(CreateDirectory(path, directory), filename);
    }

    private string CreateDirectory(string path, string directory)
    {
        var directoryPath = Path.Combine(path, directory);

        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        return directoryPath;
    }

    public async Task<string> SaveFileAsync(string directory, string filename, IFormFile file)
    {
        var path = CreateDirectory(GetRootPath(), directory, filename);
        if (File.Exists(path))
        {
            throw new FileAlreadyExistsException();
        }   
        try
        {
            await using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Position = 0;
            return path;
        }
        catch (Exception e)
        {
            throw new SaveFileException(e.Message);
        }
    }

    public async Task DeleteFile(string path)
    {
        var fullPath = Path.Combine(_env.ContentRootPath, "wwwroot", path);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }
}