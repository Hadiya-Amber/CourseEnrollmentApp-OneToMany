using Microsoft.EntityFrameworkCore;
using Restuarant_Management.Models;

namespace Restuarant_Management.Data
{
    public class RestaurantDbContext : DbContext
    {
        public DbSet<Restaurant>? Restaurants { get; set; }
        public DbSet<Chef>? Chefs { get; set; }
        public DbSet<Cuisine>? Cuisines { get; set; }
        public DbSet<UserRestaurant>? UserRestaurants { get; set; }
        public DbSet<User>? Users { get; set; }

        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite Key for UserRestaurant
            modelBuilder.Entity<UserRestaurant>()
                .HasKey(ur => new { ur.UserId, ur.RestaurantId });

            modelBuilder.Entity<UserRestaurant>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRestaurants)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRestaurant>()
                .HasOne(ur => ur.Restaurant)
                .WithMany(r => r.UserRestaurant)
                .HasForeignKey(ur => ur.RestaurantId);

            // Default Current Date for Date fields
            modelBuilder.Entity<User>()
                .Property(u => u.RegisteredDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Restaurant>()
                .Property(r => r.EstablishedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Chef>()
                .Property(c => c.JoinedDate)
                .HasDefaultValueSql("GETDATE()");

            // Seed Data - Restaurants
            modelBuilder.Entity<Restaurant>().HasData(
                new Restaurant { RestaurantId = 1, RestuarantName = "Spice Garden", Location = "Bangalore" },
                new Restaurant { RestaurantId = 2, RestuarantName = "Tandoori Treats", Location = "Hyderabad" },
                new Restaurant { RestaurantId = 3, RestuarantName = "Ocean Breeze", Location = "Chennai" },
                new Restaurant { RestaurantId = 4, RestuarantName = "Royal Curry House", Location = "Delhi" }
            );

            // Seed Data - Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "admin",
                    PasswordHash = "hash_admin123", // Dummy hash
                    Role = "Admin",
                    RegisteredDate = new DateTime(2025, 01, 01)
                },
                new User
                {
                    UserId = 2,
                    Username = "john",
                    PasswordHash = "hash_john123", // Dummy hash
                    Role = "Customer",
                    RegisteredDate = new DateTime(2025, 02, 15)
                },
                new User
                {
                    UserId = 3,
                    Username = "emma",
                    PasswordHash = "hash_emma123", // Dummy hash
                    Role = "Manager",
                    RegisteredDate = new DateTime(2025, 03, 10)
                }
            );

            // Seed Data - Chefs
            modelBuilder.Entity<Chef>().HasData(
                new Chef
                {
                    ChefId = 1,
                    ChefName = "Chef Antonio",
                    ExperienceYears = 15,
                    JoinedDate = new DateTime(2020, 01, 10),
                    Salary = 85000.00m,
                    Specialty = "Italian Cuisine",
                    RestaurantId = 1
                },
                new Chef
                {
                    ChefId = 2,
                    ChefName = "Chef Priya",
                    ExperienceYears = 10,
                    JoinedDate = new DateTime(2021, 06, 20),
                    Salary = 60000.00m,
                    Specialty = "Indian Curries",
                    RestaurantId = 2
                },
                new Chef
                {
                    ChefId = 3,
                    ChefName = "Chef Zhang Wei",
                    ExperienceYears = 12,
                    JoinedDate = new DateTime(2019, 11, 05),
                    Salary = 72000.00m,
                    Specialty = "Chinese Stir-Fry",
                    RestaurantId = 1
                }
            );

            // Seed Data - Cuisines
            modelBuilder.Entity<Cuisine>().HasData(
                new Cuisine
                {
                    CuisineId = 1,
                    CuisineName = "Italian",
                    DishName = "Pasta Alfredo",
                    Price = 350.00m,
                    IsVegetarian = true,
                    ChefId = 1
                },
                new Cuisine
                {
                    CuisineId = 2,
                    CuisineName = "Indian",
                    DishName = "Butter Chicken",
                    Price = 400.00m,
                    IsVegetarian = false,
                    ChefId = 2
                },
                new Cuisine
                {
                    CuisineId = 3,
                    CuisineName = "Chinese",
                    DishName = "Kung Pao Chicken",
                    Price = 380.00m,
                    IsVegetarian = false,
                    ChefId = 3
                },
                new Cuisine
                {
                    CuisineId = 4,
                    CuisineName = "Indian",
                    DishName = "Paneer Tikka",
                    Price = 300.00m,
                    IsVegetarian = true,
                    ChefId = 2
                }
            );
        }
    }
}
