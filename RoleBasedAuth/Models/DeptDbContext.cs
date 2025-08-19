using Microsoft.EntityFrameworkCore;

namespace Tokens.Models
{
    public class DeptDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }

        public DeptDbContext(DbContextOptions<DeptDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().ToTable("Departments");
            modelBuilder.Entity<User>().ToTable("Users");
            // Seed data for Departments
            modelBuilder.Entity<Department>().HasData(
                new Department { DeptId = 1, DeptName = "HR" },
                new Department { DeptId = 2, DeptName = "IT" },
                new Department { DeptId = 3, DeptName = "Finance" }
            );
            // Seed data for Users
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Name = "Amber", Email = "amber@gmail.com", Password = "amber123", Role = "Admin" },
                new User { UserId = 2, Name = "Charan", Email = "charan@g,mail.com", Password = "charan123", Role = "User" },
                new User { UserId = 3, Name = "Hadiya", Email = "hadiya@gmail.com", Password = "hadiya123", Role = "User" });
        }
    }
}
