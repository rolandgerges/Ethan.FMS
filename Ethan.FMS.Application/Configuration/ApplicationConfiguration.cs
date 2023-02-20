using System.Reflection;
using Ethan.FMS.Application.Services.FileSystemService;
using Ethan.FMS.Application.Services.TokenService;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Ethan.FMS.Application.Configuration;

public static  class ApplicationConfiguration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient<IFileSystemService, FileSystemService>();
        services.AddTransient<ITokenService, TokenService>();
    }
}