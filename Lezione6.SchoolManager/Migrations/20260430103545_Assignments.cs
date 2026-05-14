using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lezione6.SchoolManager.Migrations
{
    /// <inheritdoc />
    public partial class Assignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Modules_ModulesModuleId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Teachers_TeachersTeacherId",
                table: "Assignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_TeachersTeacherId",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "TeachersTeacherId",
                table: "Assignments",
                newName: "TeacherId");

            migrationBuilder.RenameColumn(
                name: "ModulesModuleId",
                table: "Assignments",
                newName: "ModuleId");

            migrationBuilder.AddColumn<int>(
                name: "AssignedHours",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments",
                columns: new[] { "TeacherId", "ModuleId" });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ModuleId",
                table: "Assignments",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Modules_ModuleId",
                table: "Assignments",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "ModuleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Teachers_TeacherId",
                table: "Assignments",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Modules_ModuleId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Teachers_TeacherId",
                table: "Assignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_ModuleId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "AssignedHours",
                table: "Assignments",
                newName: "TeachersTeacherId");

            migrationBuilder.RenameColumn(
                name: "ModuleId",
                table: "Assignments",
                newName: "ModulesModuleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments",
                columns: new[] { "ModulesModuleId", "TeachersTeacherId" });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_TeachersTeacherId",
                table: "Assignments",
                column: "TeachersTeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Modules_ModulesModuleId",
                table: "Assignments",
                column: "ModulesModuleId",
                principalTable: "Modules",
                principalColumn: "ModuleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Teachers_TeachersTeacherId",
                table: "Assignments",
                column: "TeachersTeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
