using ReportingSystem.API.DTO.Request;
using ReportingSystem.API.DTO.Request.Common;
using ReportingSystem.API.DTO.Response;
using ReportingSystem.API.DTO.Response.Common;
using ReportingSystem.API.Enums;
using ReportingSystem.API.Models;

namespace ReportingSystem.API.Services.IServices
{
    public interface IPatientReportService
    {
        Task<List<PatientReportResponse>> GetPatientReport();
        Task<PatientReportResponse> AddPatientReport(PatientReportRequest request);
    }
}
