using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restuarant_Management.Migrations
{
    /// <inheritdoc />
    public partial class Restaurant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestuarantName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EstablishedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    AverageMealPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.RestaurantId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Chefs",
                columns: table => new
                {
                    ChefId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChefName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false),
                    JoinedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Specialty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chefs", x => x.ChefId);
                    table.ForeignKey(
                        name: "FK_Chefs_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRestaurants",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRestaurants", x => new { x.UserId, x.RestaurantId });
                    table.ForeignKey(
                        name: "FK_UserRestaurants_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRestaurants_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cuisines",
                columns: table => new
                {
                    CuisineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CuisineName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DishName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsVegetarian = table.Column<bool>(type: "bit", nullable: false),
                    ChefId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuisines", x => x.CuisineId);
                    table.ForeignKey(
                        name: "FK_Cuisines_Chefs_ChefId",
                        column: x => x.ChefId,
                        principalTable: "Chefs",
                        principalColumn: "ChefId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "RestaurantId", "AverageMealPrice", "Location", "Rating", "RestuarantName" },
                values: new object[,]
                {
                    { 1, 0m, "Bangalore", 0, "Spice Garden" },
                    { 2, 0m, "Hyderabad", 0, "Tandoori Treats" },
                    { 3, 0m, "Chennai", 0, "Ocean Breeze" },
                    { 4, 0m, "Delhi", 0, "Royal Curry House" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "PasswordHash", "RegisteredDate", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "hash_admin123", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "admin" },
                    { 2, "hash_john123", new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Customer", "john" },
                    { 3, "hash_emma123", new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manager", "emma" }
                });

            migrationBuilder.InsertData(
                table: "Chefs",
                columns: new[] { "ChefId", "ChefName", "ExperienceYears", "JoinedDate", "RestaurantId", "Salary", "Specialty" },
                values: new object[,]
                {
                    { 1, "Chef Antonio", 15, new DateTime(2020, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 85000.00m, "Italian Cuisine" },
                    { 2, "Chef Priya", 10, new DateTime(2021, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 60000.00m, "Indian Curries" },
                    { 3, "Chef Zhang Wei", 12, new DateTime(2019, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 72000.00m, "Chinese Stir-Fry" }
                });

            migrationBuilder.InsertData(
                table: "Cuisines",
                columns: new[] { "CuisineId", "ChefId", "CuisineName", "DishName", "IsVegetarian", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Italian", "Pasta Alfredo", true, 350.00m },
                    { 2, 2, "Indian", "Butter Chicken", false, 400.00m },
                    { 3, 3, "Chinese", "Kung Pao Chicken", false, 380.00m },
                    { 4, 2, "Indian", "Paneer Tikka", true, 300.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chefs_RestaurantId",
                table: "Chefs",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuisines_ChefId",
                table: "Cuisines",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRestaurants_RestaurantId",
                table: "UserRestaurants",
                column: "RestaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cuisines");

            migrationBuilder.DropTable(
                name: "UserRestaurants");

            migrationBuilder.DropTable(
                name: "Chefs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}
