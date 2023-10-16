using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportingSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class add_DCom_FileName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "XRayFileName",
                table: "PatientReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "XRayFileName",
                table: "PatientReports");
        }
    }
}
