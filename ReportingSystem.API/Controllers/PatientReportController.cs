﻿using AutoMapper;
using ReportingSystem.API.Contants;
using ReportingSystem.API.DTO.Request;
using ReportingSystem.API.DTO.Request.Common;
using ReportingSystem.API.DTO.Response;
using ReportingSystem.API.DTO.Response.Common;
using ReportingSystem.API.Enums;
using ReportingSystem.API.Repository.IRepository;
using ReportingSystem.API.Services;
using ReportingSystem.API.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using iText.Html2pdf;
using iText.IO.Source;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;

namespace ReportingSystem.API.Controllers
{
    [Route(StaticValues.APIPrefix)]
    [ApiController]
    public class PatientReportController : ControllerBase
    {
        #region Private Members
        private readonly IPatientReportService _patientReportService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion

        #region CTOR
        public PatientReportController(IPatientReportService patientReportService, IWebHostEnvironment webHostEnvironment)
        {
            _patientReportService = patientReportService;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Public Methods
        //[HttpGet]
        //[Route("get/feedback")]
        //public async Task<List<FeedbackResponse>> GetFeedback()
        //{
        //    return await _feedbackService.GetFeedback();
        //}
        //[HttpGet]
        //[Route("get/dashboardCount")]
        //public async Task<DashboardResponse> GetDashboardCount()
        //{
        //    return await _feedbackService.GetDashboardCount();
        //}
        //[HttpGet]
        //[Route("newsupdate/get/{Id}")]
        //public async Task<NewsUpdateResponse> GetNewsUpdateById([FromRoute] int Id)
        //{
        //    return await _feedbackService.GetNewsUpdateById(Id);
        //} 
        [HttpPost]
        [Route("add/ReportingSystem")]
        public async Task<IActionResult> AddPatientReport([FromBody] PatientReportRequest request)
        {
            var patientData = await _patientReportService.AddPatientReport(request);
            var htmlContent = GetHTMLString(patientData);
            //ChromePdfRenderer renderer = new ChromePdfRenderer();
            //PdfDocument pdf = renderer.RenderHtmlAsPdf(htmlContent);
            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "DownloadReport", "PatientReport.pdf");
            //pdf.SaveAs(filePath);

            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(htmlContent)))
            {
                ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
                PdfWriter writer = new PdfWriter(byteArrayOutputStream);
                PdfDocument pdfDocument = new PdfDocument(writer);
                pdfDocument.SetDefaultPageSize(PageSize.A4);
                HtmlConverter.ConvertToPdf(stream, pdfDocument);
                pdfDocument.Close();
                return File(byteArrayOutputStream.ToArray(), "application/pdf", "Patient_Report.pdf");
            }




            //string webRootPath = _webHostEnvironment.WebRootPath;
            //string outputFilePath = filePath;

            //if (!System.IO.File.Exists(outputFilePath))
            //{
            //    // Return a 404 Not Found error if the file does not exist
            //    return NotFound();
            //}

            //var fileInfo = new System.IO.FileInfo(outputFilePath);
            //Response.ContentType = "application/pdf";
            //Response.Headers.Add("Content-Disposition", "attachment;filename=\"" + fileInfo.Name + "\"");
            //Response.Headers.Add("Content-Length", fileInfo.Length.ToString());

            //// Send the file to the client
            //return File(System.IO.File.ReadAllBytes(outputFilePath), "application/pdf", fileInfo.Name);

        }
        private string GetHTMLString(PatientReportResponse patientData)
        {
            //var employees = DataStorage.GetAllEmployess();
            //{% static 'images/SNMC.jpg' %}
            var sb = new StringBuilder();
            sb.Append(@"<!DOCTYPE html>
                        <html lang='en'>
                        <head>
                            <title>Chest XRay Report</title>
                        <style>
                            .mainContainer {
                            margin: 0px 20px;
                            }
                            body {
                            color: #000000;
                            background: #ffffff;
                            font-family: Arial;
                            }
                            .hostipaldetails {
                            text-align: center;
                            }

                            .hostipaldetails p {
                            font-size: 17px;
                            margin: 0px !important;
                            }

                            .uhidhead p{
                            padding-bottom: 0px !important;
                            margin: 0px !important;
                            }

                            #logo {
                            padding: 0px 0px 0px 0px;
                            }

                            #logo img {
                            width: 70px;
                            }
                            .state {
                            padding-top: 5px;
                            }
                            #patientDetails p{
                            font-size:14px;
                            padding: 0px !important;
                            margin: 0px !important;
                            display: flex;
                            justify-content: space-between;
                            }
                            #results p{
                            font-size: 13px;
                            padding: 0px !important;
                            margin: 0px !important;
                            }
                            table {
	                            width: 100%;
	                            border-collapse: collapse;
                            }
                            th, td {
	                            font-size: 14px;
	                            padding: 2px;
	                            text-align: left;
                            }
                            footer{
	                            float: right;
	                            text-align: right;
                            }
                            </style>
                        </head>");
            sb.AppendFormat(@"<body>
                            <div class='mainContainer'>
                                <table>
                                    <tr>
                                        <td>
                                            <img src='http://api.imgdotpix.in/Asset/logo.jpg' class='brand-logo' width='70' alt='My image' height='70'>
                                        </td>
                                        <td style='padding-left:400px;'>
                                            <p style='margin:1px;'>Department of Radiodiagnosis</p>
                                            <p style='margin:1px;'>S N MEDICAL COLLEGE</p>
                                            <p style='margin:1px;' class='state'>Agra, Uttar Pradesh</p>
                                        </td>
                                    </tr>
                                </table>
                                <table style='width: 100%; border: 1px solid black;'>
                                    <tr>
                                        <td>Name</td>
                                        <td><b>{0} </b>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            Gender/Sex
                                        </td>
                                        <td>
                                            <b> {1}</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            UHID/Patient ID
                                        </td>
                                        <td>
                                            <b>{2} </b>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            Reff By
                                        </td>
                                        <td>
                                            <b> {3}</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Age
                                        </td>
                                        <td>
                                            <b>{4}</b>
                                        </td>
                                        <td>
                                            Date
                                        </td>
                                        <td>
                                            <b>{5}</b>
                                        </td>
                                    </tr>
                                </table>", patientData.FullName, patientData.gender, patientData.uhid, patientData.refby, patientData.age, patientData.date.Value.ToLongDateString());
            sb.AppendFormat(@"<hr>
                              <div id='results'>
                                <p style='text-align: center;'>Investigation Report</p>
                                <p style='padding: 5px; text-decoration: underline;'><b>Lung Field:</b></p>");


            if (patientData.BronchoVascularMarking == "Normal")
                sb.AppendFormat(@"<p> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The Broncho Vascular Markings appears {0}</p>", patientData.BronchoVascularMarking);
            else
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The Broncho Vascular Markings are <b>{0}</b> in <b>{1} {2}</b> Lung.</p>", patientData.BronchoVascularMarking, patientData.BronchoVascularMarkingSide, patientData.BronchoVascularMarkingRegion);

            if (patientData.opacity == "Absent")
                sb.AppendFormat(@"<p> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; No Opacity found in either of the lung.</p>");
            else if (patientData.opacity == "Present")
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The opacity is <b>{0}</b> in <b>{1} {2} lung.</b></p>", patientData.opacity, patientData.opacitySide, patientData.opacityRegion);

            if (patientData.cavity == "Absent")
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No cavity found in either of the lung.</p>");
            else if (patientData.cavity == "Present")
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The cavity is <b>{0}</b> in <b>{1} {2} lung.</b></p>", patientData.cavity, patientData.cavitySide, patientData.cavityRegion);

            if (patientData.masses == "Absent")
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No masses found in either of the lung.</p>");
            else if (patientData.masses == "Present")
                sb.AppendFormat(@" <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The masses is <b>{0}</b> in <b>{1} {2} lung.</b></p>", patientData.masses, patientData.massesSide, patientData.massesRegion);

            if (patientData.hilumSide == "Normal")
                sb.AppendFormat(@" <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The hilum in {0} lung appears {1}.</p>", patientData.hilumSide, patientData.hilum);
            else
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The hilum in <b>{0}</b> lung shows <b>{1} {2}</b>.</p>", patientData.hilumSide, patientData.hilum, patientData.ProminentHilumSpecify);

            sb.AppendFormat(@"<p style='padding: 5px; text-decoration: underline;'><b>Mediastinum Info:</b></p>");

            if (patientData.trachea == "Central")
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The trachea is {0}</p>", patientData.trachea);
            else
                sb.AppendFormat(@" <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The trachea is {0}ed towards {1} </p>", patientData.trachea, patientData.tracheaShiftSide);

            if (patientData.mediastinal == "Shift")
                sb.AppendFormat(@" <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The mediastinal is {0}ed towards {1} </p>", patientData.mediastinal, patientData.mediastinalShiftSide);
            else if (patientData.mediastinal == "Nil")
                sb.AppendFormat(@"");
            else
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The mediastinal is {0} </p>", patientData.mediastinal);

            if (patientData.LymphNodes == "Enlarged")
                sb.AppendFormat(@" <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The Lymph Nodes are {0}</p>", patientData.LymphNodes);

            sb.AppendFormat(@"<p style='padding: 5px; text-decoration: underline;'><b>Cadiac Info:</b></p>");

            if (patientData.CardiacSize == "Nil")
                sb.AppendFormat(@"");
            else
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The Cardiac Size is {0}</p>", patientData.CardiacSize);

            if (patientData.CardiacShape == "Nil")
                sb.AppendFormat(@"");
            else if (patientData.CardiacShape == "Abnormal")
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The Cardiac Shape is {0}</p>", patientData.CardiacShapeAbnormal);
            else
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The Cardiac Shape is {0}</p>", patientData.CardiacShape);

            if (patientData.AorticKnuckle == "Nil")
                sb.AppendFormat(@"");
            else
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The Aortic Knuckle is {0}</p>", patientData.AorticKnuckle);

            if (patientData.AorticKnuckleCalcification == "Nil")
                sb.AppendFormat(@"");
            else
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The Aortic Knuckle Calcification is {0}</p>", patientData.AorticKnuckleCalcification);

            if (patientData.AorticKnuckleUnfoldingofAorta == "Nil")
                sb.AppendFormat(@"");
            else
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The Aortic Knuckle Unfolding of Aorta is {0}</p>", patientData.AorticKnuckleUnfoldingofAorta);

            sb.AppendFormat(@"<p style='padding: 5px;text-decoration: underline;'><b>Pleura Infomation:</b></p>");
            sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The Costophrenic Angles in <b>{0}</b> lung is <b>{1}</b>. </p>", patientData.CostophrenicAnglesSide, patientData.CostophrenicAngles);

            if (patientData.Pneumothorax == "Nil")
                sb.AppendFormat(@"");
            else
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The Pneumothorax is <b>{0}</b> in <b>{1}</b> lung.</p>", patientData.Pneumothorax, patientData.PneumothoraxSide);

            sb.AppendFormat(@"<p style='padding: 5px; text-decoration: underline;'><b>Chest Wall Info:</b></p>");

            if (patientData.BonyCage == "Nil")
                sb.AppendFormat(@"");
            else if (patientData.BonyCage == "Normal")
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The Bony Cage is Normal.</p>");
            else if (patientData.Finding == "Crowding of Ribs")
                sb.AppendFormat(@" <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The Bony Cage of {0} lung is {1}</p>", patientData.BonyCageSide, patientData.Finding);
            else if (patientData.Finding == "Fracture")
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The Bony Cage of {0} lung is {1} and Fracture Rib Number is <b>{2}</b>.</p>", patientData.BonyCageSide, patientData.Finding, patientData.FractureRibNumber);
            else if (patientData.Finding == "Dysplasia of Ribs")
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The Bony Cage of {0} lung shows {1} and the Rib Number is <b>{2}</b>. </p>", patientData.BonyCageSide, patientData.Finding, patientData.FractureRibNumber);
            else if (patientData.Finding == "Bony Lesion")
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The Bony Cage of {0} lung shows <b>{1}</b> {2}. </p>", patientData.BonyCageSide, patientData.Bonylesion, patientData.Finding);

            if (patientData.SoftTissue == "Normal")
                sb.AppendFormat(@" <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The Soft Tissue appears {0}.</p>", patientData.SoftTissue);
            else
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The Soft Tissue shows <b>{0}</b> side <b>{1}</b>.</p>", patientData.SoftTissueSide, patientData.SoftTissueAbnormal);

            if (patientData.HemiDiaphragm == "Normal")
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The Hemi Diaphragm in {0} side appears {1}.</p>", patientData.HemiDiaphragmSide, patientData.HemiDiaphragm);
            else
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The Hemi Diaphragm in <b>{0}</b> side shows <b>{1}</b>.</p>", patientData.HemiDiaphragmSide, patientData.HemiDiaphragmAbormal);

            if (patientData.BreastShadow == "Nil")
                sb.AppendFormat(@"");
            else if (patientData.BreastShadow == "Normal")
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The Breast Shadow is {0}</p>", patientData.BreastShadow);
            else
                sb.AppendFormat(@"<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The Breast Shadow of <b>{0}</b> side shows <b>{1}</b>.</p>", patientData.BreastShadowSide, patientData.BreastShadowAbnormal);

            sb.AppendFormat(@"<br> <p><b><u>IMPRESSION</u>:</b>&nbsp; X-ray images are suggestive of :-</p>");

            if (patientData.BronchoVascularMarking == "Prominent" && patientData.opacity == "Present" && patientData.cavity == "Present" && patientData.masses == "Present")
                sb.AppendFormat(@"<p> - &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b>Broncho Vascular Markings, opacity, masses and cavity are present and and trachea is {0}</b></p>", patientData.trachea);
            else if (patientData.BronchoVascularMarking == "Normal")
                sb.AppendFormat(@"<p> - &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Broncho Vascular Marking appears Normal, hilum in {0} lung appears {1} and trachea is {2}.</p>", patientData.hilumSide, patientData.hilum, patientData.trachea);

            if (patientData.LymphNodes == "Enlarged")
                sb.AppendFormat(@"<p> - &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Lymph Nodes are {0}</p>", patientData.LymphNodes);

            if (patientData.CardiacSize != "Nil" && patientData.CardiacShape != "Nil")
                sb.AppendFormat(@"<p> - &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b>Cardiac size is {0} and Cardiac Shape is {1}</b>.</p>", patientData.CardiacSize, patientData.CardiacShape);
            else if (patientData.CardiacSize != "Nil" && patientData.CardiacShape == "Nil")
                sb.AppendFormat(@"<p> - &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Cardiac size is {0}</p>", patientData.CardiacSize);

            sb.AppendFormat(@"<p>- &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Costophrenic Angles in <b>{0}</b> lung is <b>{1}</b> and Hemi Diaphragm in {2} side appears {3} </p>", patientData.CostophrenicAnglesSide, patientData.CostophrenicAngles, patientData.HemiDiaphragmSide, patientData.HemiDiaphragm);

            sb.Append(@"<br>
                        <p><b>Please correlate clinically</b></p>
                        <footer class='footer'>
                            <p>Doctor's Name</p>
                            <p>Signature / Date</p>
                        </footer>

                      </div>
                    </div> <table style='width: 100%; border: 1px solid black;'>
                    <tr>
                        <td colspan='2'>
                            Authorised Signatory
                        </td>
                    </tr>
                    <tr>
                        <td colspan='2'>
                           This Report to be matched clinically, This is not for madico legel perpose 
                        </td>
                    </tr>
                </table>");

            return sb.ToString();
        }
        //[HttpDelete]
        //[Route("delete")]
        //public async Task<bool> DeleteNewsUpdate([FromQuery] int id)
        //{
        //    return await _feedbackService.DeleteNewsUpdate(id);
        //}
        #endregion
    }
}
