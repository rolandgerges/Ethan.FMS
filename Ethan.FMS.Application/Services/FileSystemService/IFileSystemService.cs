using Microsoft.AspNetCore.Http;

namespace Ethan.FMS.Application.Services.FileSystemService;

public interface IFileSystemService
{
     Task<string> SaveFileAsync(string directory, string filename, IFormFile file);
     Task DeleteFile(string path);
}