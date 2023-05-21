using Diploma.model.employee;
using Diploma.model.order;
using Diploma.model.product;
using Diploma.model.provider;
using Diploma.model.supply;
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
    public virtual DbSet<Provider> Providers { get; set; }
    public virtual DbSet<Warehouse> Warehouses { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<Product> Products { get; set; } 
    public virtual DbSet<Supply> Supplies { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
}