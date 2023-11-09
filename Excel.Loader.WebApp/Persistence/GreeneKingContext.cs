using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Excel.Loader.WebApp.Persistence;

public partial class GreeneKingContext : DbContext
{
    public GreeneKingContext()
    {
    }

    public GreeneKingContext(DbContextOptions<GreeneKingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ControlFlow> ControlFlows { get; set; }

    public virtual DbSet<DataFlow> DataFlows { get; set; }

    public virtual DbSet<Destination> Destinations { get; set; }

    public virtual DbSet<DestinationTransformation> DestinationTransformations { get; set; }

    public virtual DbSet<Executable> Executables { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobsHistory> JobsHistories { get; set; }

    public virtual DbSet<Mapping> Mappings { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<PackageParameter> PackageParameters { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Source> Sources { get; set; }

    public virtual DbSet<SourceTransformation> SourceTransformations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ControlFlow>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.ControlFlow1)
                .HasColumnType("image")
                .HasColumnName("ControlFlow");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DataFlow>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.DataFlow1)
                .HasColumnType("image")
                .HasColumnName("DataFlow");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Destination>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.DatabaseOrFilePath)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DestinationType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Server)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.PackageNameNavigation).WithMany()
                .HasForeignKey(d => d.PackageName)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Destinations_Packages");
        });

        modelBuilder.Entity<DestinationTransformation>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.ColumnName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DatabaseOrFilePath)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Read)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Server)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TableName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Write)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.PackageNameNavigation).WithMany()
                .HasForeignKey(d => d.PackageName)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DestinationTransformations_Packages");
        });

        modelBuilder.Entity<Executable>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.ExecutableName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ExecutableType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ExecutedOnDatabase)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ExecutedOnServer)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.PackageNameNavigation).WithMany()
                .HasForeignKey(d => d.PackageName)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Executables_Packages");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Frequency)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.JobName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastUsed).HasColumnType("datetime");
            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.PackageNameNavigation).WithMany()
                .HasForeignKey(d => d.PackageName)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Jobs_Packages");
        });

        modelBuilder.Entity<JobsHistory>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("JobsHistory");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.JobName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastRunDateTime).HasColumnType("datetime");
            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StepName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.PackageNameNavigation).WithMany()
                .HasForeignKey(d => d.PackageName)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_JobsHistory_Packages");
        });

        modelBuilder.Entity<Mapping>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.DestinationDatabase)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DestinationServer)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DestinationTable)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DestinationTableColumn)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SourceDatabase)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SourceTable)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SourceTableColumn)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.PackageNameNavigation).WithMany()
                .HasForeignKey(d => d.PackageName)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Mapping_Packages");
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.PackageName).HasName("PK__Packages__73856F7B25999289");

            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Author)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ChildPackages)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Location)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Technology)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PackageParameter>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ParameterName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ParameterType)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.PackageNameNavigation).WithMany()
                .HasForeignKey(d => d.PackageName)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Parameters_Packages");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProjectName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.PackageNameNavigation).WithMany()
                .HasForeignKey(d => d.PackageName)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Projects_Packages");
        });

        modelBuilder.Entity<Source>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.DatabaseOrFilePath)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Server)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SourceType)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.PackageNameNavigation).WithMany()
                .HasForeignKey(d => d.PackageName)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Sources_Packages");
        });

        modelBuilder.Entity<SourceTransformation>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.ColumnName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DatabaseOrFilePath)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Read)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Server)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TableName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Write)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.PackageNameNavigation).WithMany()
                .HasForeignKey(d => d.PackageName)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SourceTransformations_Packages");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
