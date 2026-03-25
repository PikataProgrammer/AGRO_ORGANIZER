using AgroOrganizer.Models.Entities.Contracts;
using AgroOrganizer.Models.Entities.Drivers;
using AgroOrganizer.Models.Entities.Field;
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
    
}
