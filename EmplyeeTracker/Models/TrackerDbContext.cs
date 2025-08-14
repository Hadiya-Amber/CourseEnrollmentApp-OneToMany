using Microsoft.EntityFrameworkCore;

namespace EPTrackerApi.Models
{
    public class TrackerDbContext:DbContext
    {
        public DbSet<Project>Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public TrackerDbContext(DbContextOptions<TrackerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Unique constraints
            modelBuilder.Entity<Project>()
                .HasIndex(p => p.ProjectCode)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.EmployeeCode)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();

            // Relationship
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Project)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // Decimal precision (prevents EF warning about Salary/Budget)
            modelBuilder.Entity<Project>()
                .Property(p => p.Budget)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasPrecision(18, 2);

            // Seeding of data
            modelBuilder.Entity<Project>().HasData(
                new Project { ProjectId = 1, ProjectCode = "PRJ001", ProjectName = "Project Alpha", StartDate =new DateTime(2025, 08, 13)
,
                    Budget = 100000 },
                new Project { ProjectId = 2, ProjectCode = "PRJ002", ProjectName = "Project Beta", StartDate = new DateTime(2025, 08, 13), Budget = 200000 }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, EmployeeCode = "EMP001", FullName = "Hadiya", Email = "hadiya@gmail.com", Designation = "Software Engineer", Salary = 60000, ProjectId = 1 },
                new Employee { EmployeeId = 2, EmployeeCode = "EMP002", FullName = "Charan", Email = "charan@gmail.com", Designation = "Project Manager", Salary = 80000, ProjectId = 2 },
                new Employee { EmployeeId = 3, EmployeeCode = "EMP003", FullName = "Amber", Email = "amber@gmail.com", Designation = "Business Analyst", Salary = 70000, ProjectId = 1 }
            );

            // Call base only once at the end
            base.OnModelCreating(modelBuilder);
        }

    }
}
