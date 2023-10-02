using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportingSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class mobile_DataType_Changed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "mobileNo",
                table: "PatientReports",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "mobileNo",
                table: "PatientReports",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
