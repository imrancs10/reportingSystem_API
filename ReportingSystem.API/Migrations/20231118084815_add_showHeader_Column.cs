using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportingSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class add_showHeader_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowHeader",
                table: "Organizations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowHeader",
                table: "Organizations");
        }
    }
}
