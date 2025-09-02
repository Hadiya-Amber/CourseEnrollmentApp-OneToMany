using ApiAssessment.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiAssessment.Data
{
    public class EmpDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmpPro> EmpPros { get; set; }

        public EmpDbContext(DbContextOptions<EmpDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmpPro>()
                .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });
            modelBuilder.Entity<EmpPro>()
                .HasOne(ep => ep.Employee)
                .WithMany(e => e.EmpPros)
                .HasForeignKey(ep => ep.EmployeeId);
            modelBuilder.Entity<EmpPro>()
                .HasOne(ep => ep.Project)
                .WithMany(p => p.EmpPros)
                .HasForeignKey(ep => ep.ProjectId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
