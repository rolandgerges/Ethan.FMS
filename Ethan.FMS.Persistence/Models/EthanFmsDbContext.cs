using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Ethan.FMS.Persistence.Models;

public partial class EthanFmsDbContext : DbContext
{
    public EthanFmsDbContext()
    {
    }

    public EthanFmsDbContext(DbContextOptions<EthanFmsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Files> Files { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Files>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__File__3214EC0793FEE188");

            entity.ToTable("File");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
