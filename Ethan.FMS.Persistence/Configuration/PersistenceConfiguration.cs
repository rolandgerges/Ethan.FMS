using Ethan.FMS.Persistence.Configuration.Settings;
using Ethan.FMS.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ethan.FMS.Persistence.Configuration;

public static class PersistenceConfiguration
{
    private static StorageSettings _storageSettings;
    public static void AddPersistenceSettings(this IServiceCollection services, IConfiguration configuration)
    {
        _storageSettings = new StorageSettings();
        configuration.GetSection(nameof(StorageSettings)).Bind(_storageSettings);
        services.AddSingleton(_storageSettings);
       // services.AddSingleton<IValidatable>(_storageSettings);
    }

    public static void AddPersistenceServices(this IServiceCollection services)
    {
        
            services.AddDbContext<EthanFmsDbContext>(options =>
               options.UseSqlServer
               ( _storageSettings.DefaultConnection));
   
         
    }
}