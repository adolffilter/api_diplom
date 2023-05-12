using Diploma.model.specialization;
using Diploma.model.user;
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
    public virtual DbSet<Specialization> Specializations { get; set; } 
}