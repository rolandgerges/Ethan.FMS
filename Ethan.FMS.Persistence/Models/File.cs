using System;
using System.Collections.Generic;

namespace Ethan.FMS.Persistence.Models;

public partial class Files
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Path { get; set; }

    public long Size { get; set; }

    public DateTime CreatedAt { get; set; }
}
