using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class AppDbContext : DbContext
{
    public AppDbContext() {}
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<User> Users { get; set; }
    public DbSet<Url> Urls { get; set; }
    public DbSet<Constant> Constants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<Url>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<Url>()
            .HasOne(u => u.User)
            .WithMany(u => u.Urls)
            .HasForeignKey(u => u.UserId);

        modelBuilder.Entity<Constant>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Constant>()
            .HasIndex(c => c.Name)
            .IsUnique();
        
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = "admin_user",
                Login = "admin",
                Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                IsAdmin = true
            }
        );
        
        modelBuilder.Entity<Constant>().HasData(
            new Constant
            {
                Id = "about_text",
                Name = "about",
                Value = ""
            }
        );
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=DB;User Id=sa;Password=Admin@1234;MultipleActiveResultSets=true;TrustServerCertificate=True");
    }
}