using System.ComponentModel.DataAnnotations;

namespace Ethan.FMS.Persistence.Configuration.Settings;

public class StorageSettings
{
    [Required]
    public string DefaultConnection { get; set; }
}