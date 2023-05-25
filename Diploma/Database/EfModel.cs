using Diploma.model.order;
using Diploma.model.provider;
using Diploma.model.user;
using Diploma.model.warehouse;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Database;

public class EfModel:DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public EfModel(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Admin> Admins { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<WarehouseOrder> WarehouseOrders { get; set; }
    public virtual DbSet<Provider> Providers { get; set; }
    public virtual DbSet<ProviderPost> ProviderPosts { get; set; }
}