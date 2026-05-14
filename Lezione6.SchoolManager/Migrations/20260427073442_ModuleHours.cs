using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lezione6.SchoolManager.Migrations
{
    /// <inheritdoc />
    public partial class ModuleHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Hours",
                table: "Modules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hours",
                table: "Modules");
        }
    }
}
