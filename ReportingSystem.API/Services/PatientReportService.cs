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
            string orgName = !string.IsNullOrEmpty(request.orgName) && request.orgName.Length >= 4 ? request.orgName.Substring(0, 4).ToUpper() : "";
            var nameList = request.FullName.Split(" ");
            string name = nameList.Length >= 2 ? nameList[0].ToCharArray()[0].ToString().ToUpper() + nameList[1].ToCharArray()[0].ToString().ToUpper() : nameList[0].ToCharArray()[0].ToString().ToUpper() + nameList[0].ToCharArray()[1].ToString().ToUpper();
            string padNo = "1";
            var maxIdReport = _context.PatientReports.OrderByDescending(x => x.Id).FirstOrDefault();
            if (maxIdReport != null)
            {
                var uniqueId = maxIdReport.UniqueId;
                if (!string.IsNullOrEmpty(uniqueId))
                {
                    padNo = (Convert.ToInt32(uniqueId.Substring(uniqueId.Length - 4, 4)) + 1).ToString();
                }
            }

            masterData.UniqueId = orgName + Convert.ToString(DateTime.Now.Year) + name + padNo.PadLeft(4, '0');

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
