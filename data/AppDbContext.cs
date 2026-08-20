using Microsoft.EntityFrameworkCore;
using CRUD_WEBAPI.Models;

namespace CRUD_WEBAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<User> Users => Set<User>();   // <-- NEW


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, Name = "Aarav", Age = 16, Email = "aarav@example.com" },
            new Student { Id = 2, Name = "Zoya", Age = 14, Email = "zoya@example.com" }
        );


        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
    }
}