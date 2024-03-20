using Data.ModelConfigurations;
using Domain.Purchases;
using Domain.StoreItems;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class DbApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<StoreItem> StoreItems { get; set; } = null!;
    public DbSet<Purchase> Purchases { get; set; } = null!;
    public DbSet<PurchasedItem> PurchasedItems { get; set; } = null!;
    
    public DbApplicationContext()
    {
        Database.EnsureCreated();
    }

    public DbApplicationContext(DbContextOptions<DbApplicationContext> options) : base(options)
    { 
    
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new StoreItemConfiguration());
        modelBuilder.ApplyConfiguration(new PurchaseConfiguration());
        modelBuilder.ApplyConfiguration(new PurchasedItemConfiguration());
    }

    public static void ConfigureContextOptions(DbContextOptionsBuilder options, string connection)
    {
        options.UseNpgsql(connection);
    }
}