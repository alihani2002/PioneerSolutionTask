using Microsoft.EntityFrameworkCore;
using PioneerSolution.Core.Models;

namespace PioneerSolution.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Employee Dynamic Properties
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<PropertyDefinition> PropertyDefinitions { get; set; } = null!;
    public DbSet<EmployeePropertyValue> EmployeePropertyValues { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Employee configuration
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.HasIndex(e => e.Code).IsUnique();
        });

        // PropertyDefinition configuration
        modelBuilder.Entity<PropertyDefinition>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Type).IsRequired().HasMaxLength(50);
        });

        // EmployeePropertyValue configuration
        modelBuilder.Entity<EmployeePropertyValue>(entity =>
        {
            entity.HasKey(epv => epv.Id);

            entity.HasOne(epv => epv.Employee)
                .WithMany(e => e.PropertyValues)
                .HasForeignKey(epv => epv.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(epv => epv.PropertyDefinition)
                .WithMany(pd => pd.PropertyValues)
                .HasForeignKey(epv => epv.PropertyDefinitionId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

