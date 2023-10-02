using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportingSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class age_Column_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "age",
                table: "PatientReports",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "age",
                table: "PatientReports");
        }
    }
}
