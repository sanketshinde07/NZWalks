using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.Models;

public partial class NzwalkContext : DbContext
{
    public NzwalkContext()
    {
    }

    public NzwalkContext(DbContextOptions<NzwalkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Difficulty> Difficulties { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Walk> Walks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost,1433;User Id=SA;Password=Admin123;Initial Catalog=NZWalk;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Difficulty>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Walk>(entity =>
        {
            entity.HasIndex(e => e.DifficultyId, "IX_Walks_DifficultyID");

            entity.HasIndex(e => e.RegionId, "IX_Walks_RegionID");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DifficultyId).HasColumnName("DifficultyID");
            entity.Property(e => e.RegionId).HasColumnName("RegionID");

            entity.HasOne(d => d.Difficulty).WithMany(p => p.Walks).HasForeignKey(d => d.DifficultyId);

            entity.HasOne(d => d.Region).WithMany(p => p.Walks).HasForeignKey(d => d.RegionId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
