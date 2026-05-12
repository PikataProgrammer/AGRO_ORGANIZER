using AgroOrganizer.Models.Entities.Activity;
using AgroOrganizer.Models.Entities.Contracts;
using AgroOrganizer.Models.Entities.Drivers;
using AgroOrganizer.Models.Entities.Expense;
using AgroOrganizer.Models.Entities.Field;
using AgroOrganizer.Models.Entities.FieldSeason;
using AgroOrganizer.Models.Entities.Sales;
using AgroOrganizer.Models.Entities.User;
using AgroOrganizer.Models.Entities.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace AgroOrganizer.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context) : base(context)
    {
        
    }

    public DbSet<ContractEntity> Contracts => Set<ContractEntity>();
    public DbSet<DriverEntity> Drivers => Set<DriverEntity>();
    public DbSet<FieldEntity> Fields => Set<FieldEntity>();
    public DbSet<SaleEntity> Sales => Set<SaleEntity>();
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();
    public DbSet<ExpenseEntity> Expenses => Set<ExpenseEntity>();
    public DbSet<FieldSeasonEntity> FieldSeasons => Set<FieldSeasonEntity>();
    public DbSet<VehicleEntity> Vehicles => Set<VehicleEntity>();
    public DbSet<VehicleServiceEntity> VehicleServices => Set<VehicleServiceEntity>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<FieldEntity>()
            .HasMany(f => f.Seasons)
            .WithOne(s => s.Field)
            .HasForeignKey(s => s.FieldId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<FieldSeasonEntity>()
            .HasMany(s => s.Expenses)
            .WithOne(e => e.FieldSeason)
            .HasForeignKey(e => e.FieldSeasonId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FieldSeasonEntity>()
            .HasMany(s => s.Activities)
            .WithOne(a => a.FieldSeason)
            .HasForeignKey(a => a.FieldSeasonId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<ActivityEntity>()
            .HasOne(a => a.Driver)
            .WithMany(d => d.Activities)
            .HasForeignKey(a => a.DriverId)
            .OnDelete(DeleteBehavior.SetNull); 
        
        modelBuilder.Entity<FieldSeasonEntity>()
            .HasMany(s => s.Sales)
            .WithOne(sale => sale.FieldSeason)
            .HasForeignKey(sale => sale.FieldSeasonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
}
