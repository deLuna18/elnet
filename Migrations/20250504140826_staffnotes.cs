using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubdivisionManagement.Migrations
{
    /// <inheritdoc />
    public partial class staffnotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StaffNotes",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StaffNotes",
                table: "Services");
        }
    }
}
