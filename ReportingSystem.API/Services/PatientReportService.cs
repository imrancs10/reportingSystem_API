using AutoMapper;
using Azure;
using ReportingSystem.API.Contants;
using ReportingSystem.API.Data;
using ReportingSystem.API.DTO.Request;
using ReportingSystem.API.DTO.Request.Common;
using ReportingSystem.API.DTO.Response;
using ReportingSystem.API.DTO.Response.Common;
using ReportingSystem.API.DTO.Response.Image;
using ReportingSystem.API.Enums;
using ReportingSystem.API.Exceptions;
using ReportingSystem.API.Models;
using ReportingSystem.API.Repository;
using ReportingSystem.API.Repository.IRepository;
using ReportingSystem.API.Services.IServices;
using Microsoft.EntityFrameworkCore;
using ReportingSystem.API.DTO.Response;
using ReportingSystem.API.DTO.Request;

namespace ReportingSystem.API.Services
{
    public class PatientReportService : IPatientReportService
    {
        #region Private Members
        private readonly IMapper _mapper;
        private ReportingSystemContext _context;
        #endregion

        #region CTOR
        public PatientReportService(IMapper mapper,
            ReportingSystemContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        public async Task<PatientReportResponse> AddPatientReport(PatientReportRequest request)
        {
            PatientReport masterData = _mapper.Map<PatientReport>(request);
            masterData.XRayFileName = request.XRayReportFileName;
            var entity = _context.PatientReports.Add(masterData);
            entity.State = EntityState.Added;
            if (await _context.SaveChangesAsync() > 0) return _mapper.Map<PatientReportResponse>(entity.Entity);
            return default(PatientReportResponse);
        }
        public async Task<List<PatientReportResponse>> GetPatientReport()
        {
            var result = await _context.PatientReports.Where(x => !x.IsDeleted).OrderByDescending(x => x.UpdatedAt).ToListAsync();
            var res = _mapper.Map<List<PatientReportResponse>>(result);
            return res;
        }
        #endregion
    }
}
