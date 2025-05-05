using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubdivisionManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContactRequestRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StaffNotes",
                table: "ContactRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "HomeownerId1",
                table: "ContactRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactRequests_HomeownerId1",
                table: "ContactRequests",
                column: "HomeownerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactRequests_Homeowners_HomeownerId1",
                table: "ContactRequests",
                column: "HomeownerId1",
                principalTable: "Homeowners",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactRequests_Homeowners_HomeownerId1",
                table: "ContactRequests");

            migrationBuilder.DropIndex(
                name: "IX_ContactRequests_HomeownerId1",
                table: "ContactRequests");

            migrationBuilder.DropColumn(
                name: "HomeownerId1",
                table: "ContactRequests");

            migrationBuilder.AlterColumn<string>(
                name: "StaffNotes",
                table: "ContactRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
