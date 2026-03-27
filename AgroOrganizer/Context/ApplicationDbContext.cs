using AgroOrganizer.Models.Entities.Activity;
using AgroOrganizer.Models.Entities.Contracts;
using AgroOrganizer.Models.Entities.Drivers;
using AgroOrganizer.Models.Entities.Expense;
using AgroOrganizer.Models.Entities.Field;
using AgroOrganizer.Models.Entities.FieldSeason;
using AgroOrganizer.Models.Entities.Sales;
using AgroOrganizer.Models.Entities.User;
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
    
}
