using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportingSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class added_orgId_In_Report : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "PatientReports",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "PatientReports");
        }
    }
}
