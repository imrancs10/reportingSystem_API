using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportingSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class addTablePateintReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    uhid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    refby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    age = table.Column<int>(type: "int", nullable: true),
                    mobileNo = table.Column<int>(type: "int", nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BronchoVascularMarking = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BronchoVascularMarkingSide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BronchoVascularMarkingRegion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    opacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    opacitySide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    opacityRegion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cavity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cavitySide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cavityRegion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    masses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    massesSide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    massesRegion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hilum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hilumSide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProminentHilumSpecify = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trachea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tracheaShiftSide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mediastinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mediastinalShiftSide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LymphNodes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardiacSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardiacShape = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardiacShapeAbnormal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AorticKnuckle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AorticKnuckleCalcification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AorticKnuckleUnfoldingofAorta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostophrenicAngles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostophrenicAnglesSide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pneumothorax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PneumothoraxSide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BonyCage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BonyCageSide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Finding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bonylesion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FractureSide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FractureRibNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoftTissue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoftTissueSide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoftTissueAbnormal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HemiDiaphragmSide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HemiDiaphragm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HemiDiaphragmAbormal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BreastShadow = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BreastShadowSide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BreastShadowAbnormal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    DeletedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientReports", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientReports");
        }
    }
}
