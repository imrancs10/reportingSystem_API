using AutoMapper;
using ReportingSystem.API.DTO.Request;
using ReportingSystem.API.DTO.Request.Common;
using ReportingSystem.API.DTO.Request.ImageStore;
using ReportingSystem.API.DTO.Response;
using ReportingSystem.API.DTO.Response.Common;
using ReportingSystem.API.DTO.Response.Image;
using ReportingSystem.API.Models;

namespace ReportingSystem.API.Config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            #region Login

            #endregion

            #region User
            CreateMap<User, UserResponse>();
            CreateMap<PatientReport, PatientReportResponse>()
                 .ForMember(des => des.date, src => src.MapFrom(x => x.date.Value.ToString("dd/MMM/yyyy")));
            CreateMap<PatientReportRequest, PatientReport>();
            CreateMap<UserRequest, User>()
                .ForMember(des => des.EmailVerificationCode, src => src.MapFrom(x => Guid.NewGuid().ToString()))
                .ForMember(des => des.EmailVerificationCodeExpireOn, src => src.MapFrom(x => DateTime.Now.AddHours(24)));

            CreateMap<OrganizationRequest, Organization>();
            CreateMap<Organization, OrganizationResponse>();
            CreateMap<Organization, User>();
            #endregion

        }

        public static IMapper GetMapperConfig()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperConfig()); });
            return config.CreateMapper();
        }
    }
}
