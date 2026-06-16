using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitectureReference.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "restaurants");

            migrationBuilder.RenameTable(
                name: "Restaurants",
                newName: "Restaurants",
                newSchema: "restaurants");

            migrationBuilder.RenameTable(
                name: "Dishes",
                newName: "Dishes",
                newSchema: "restaurants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Restaurants",
                schema: "restaurants",
                newName: "Restaurants");

            migrationBuilder.RenameTable(
                name: "Dishes",
                schema: "restaurants",
                newName: "Dishes");
        }
    }
}
