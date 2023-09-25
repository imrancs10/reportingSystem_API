using AutoMapper;
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

namespace ReportingSystem.API.Controllers
{
    [Route(StaticValues.APIPrefix)]
    [ApiController]
    public class PatientReportController : ControllerBase
    {
        #region Private Members
        private readonly IPatientReportService _patientReportService;
        #endregion

        #region CTOR
        public PatientReportController(IPatientReportService patientReportService)
        {
            _patientReportService = patientReportService;
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
        public async Task<PatientReportResponse> AddPatientReport([FromBody] PatientReportRequest request)
        {
            return await _patientReportService.AddPatientReport(request);
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
