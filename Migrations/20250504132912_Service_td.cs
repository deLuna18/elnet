using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubdivisionManagement.Migrations
{
    /// <inheritdoc />
    public partial class Service_td : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeownerId = table.Column<int>(type: "int", nullable: true),
                    ServiceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedStaffId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Homeowners_HomeownerId",
                        column: x => x.HomeownerId,
                        principalTable: "Homeowners",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_Staffs_AssignedStaffId",
                        column: x => x.AssignedStaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_AssignedStaffId",
                table: "Services",
                column: "AssignedStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_HomeownerId",
                table: "Services",
                column: "HomeownerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
