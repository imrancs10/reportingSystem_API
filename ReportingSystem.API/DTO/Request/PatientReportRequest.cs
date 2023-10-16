using ReportingSystem.API.DTO.Base;
using ReportingSystem.API.DTO.Response.Image;
using ReportingSystem.API.Enums;
using System.Security.Principal;

namespace ReportingSystem.API.DTO.Request
{
    public class PatientReportRequest : BaseRequest
    {
        //public string AorticKnuckle { get; set; }
        //public string AorticKnuckleCalcification { get; set; }
        //public string AorticKnuckleUnfoldingofAorta { get; set; }
        //public string BonyCage { get; set; }
        //public string BreastShadow { get; set; }
        //public string BronchoVascularMarking { get; set; }
        //public string BronchoVascularMarkingRegion { get; set; }
        //public string BronchoVascularMarkingSide { get; set; }
        //public string CardiacShape { get; set; }
        //public string CardiacShapeAbnormal { get; set; }
        //public string CardiacSize { get; set; }
        //public string CostophrenicAngles { get; set; }
        //public string CostophrenicAnglesSide { get; set; }
        //public string FullName { get; set; }
        //public string HemiDiaphragm { get; set; }
        //public string HemiDiaphragmSide { get; set; }
        //public string LymphNodes { get; set; }
        //public string Pneumothorax { get; set; }
        //public string PneumothoraxSide { get; set; }
        //public string SoftTissue { get; set; }
        //public string gender { get; set; }
        //public string age { get; set; }
        //public string cavity { get; set; }
        //public string cavityRegion { get; set; }
        //public string cavitySide { get; set; }
        //public string date { get; set; }
        //public string hilum { get; set; }
        //public string hilumSide { get; set; }
        //public string masses { get; set; }
        //public string massesRegion { get; set; }
        //public string massesSide { get; set; }
        //public string mediastinal { get; set; }
        //public long mobileNo { get; set; }
        //public string opacity { get; set; }
        //public string opacityRegion { get; set; }
        //public string opacitySide { get; set; }
        //public string refby { get; set; }
        //public string trachea { get; set; }
        //public string uhid { get; set; }

        //Patient Information
        public string? FullName { get; set; }
        public string? uhid { get; set; }
        public string? refby { get; set; }
        public string? gender { get; set; }
        public string? age { get; set; }
        public long? mobileNo { get; set; }
        public DateTime? date { get; set; }
        //Lung Field Info
        public string? BronchoVascularMarking { get; set; }
        public string? BronchoVascularMarkingSide { get; set; }
        public string? BronchoVascularMarkingRegion { get; set; }
        public string? opacity { get; set; }
        public string? opacitySide { get; set; }
        public string? opacityRegion { get; set; }
        public string? cavity { get; set; }
        public string? cavitySide { get; set; }
        public string? cavityRegion { get; set; }
        public string? masses { get; set; }
        public string? massesSide { get; set; }
        public string? massesRegion { get; set; }
        public string? hilum { get; set; }
        public string? hilumSide { get; set; }
        public string? ProminentHilumSpecify { get; set; }
        //Mediastinum Info
        public string? trachea { get; set; }
        public string? tracheaShiftSide { get; set; }
        public string? mediastinal { get; set; }
        public string? mediastinalShiftSide { get; set; }
        public string? LymphNodes { get; set; }
        //Cadiac Info
        public string? CardiacSize { get; set; }
        public string? CardiacShape { get; set; }
        public string? CardiacShapeAbnormal { get; set; }
        public string? AorticKnuckle { get; set; }
        public string? AorticKnuckleCalcification { get; set; }
        public string? AorticKnuckleUnfoldingofAorta { get; set; }
        //Pleura Infomation
        public string? CostophrenicAngles { get; set; }
        public string? CostophrenicAnglesSide { get; set; }
        public string? Pneumothorax { get; set; }
        public string? PneumothoraxSide { get; set; }
        //Chest Wall Info
        public string? BonyCage { get; set; }
        public string? BonyCageSide { get; set; }
        public string? Finding { get; set; }
        public string? Bonylesion { get; set; }
        public string? FractureSide { get; set; }
        public int? FractureRibNumber { get; set; }
        public string? SoftTissue { get; set; }
        public string? SoftTissueSide { get; set; }
        public string? SoftTissueAbnormal { get; set; }
        public string? HemiDiaphragmSide { get; set; }
        public string? HemiDiaphragm { get; set; }
        public string? HemiDiaphragmAbormal { get; set; }
        public string? BreastShadow { get; set; }
        public string? BreastShadowSide { get; set; }
        public string? BreastShadowAbnormal { get; set; }
        public string? orgLogoName { get; set; }
        public string? XRayReportFileName { get; set; }
        public string? XRayReportBase64 { get; set; }
    }
}
