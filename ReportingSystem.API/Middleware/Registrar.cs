using ReportingSystem.API.Config;
using ReportingSystem.API.Data;
using ReportingSystem.API.Repositories;
using ReportingSystem.API.Services;
using ReportingSystem.API.Services.Interfaces;
using ReportingSystem.API.Services.IServices;
using ReportingSystem.API.Utility;
using Microsoft.EntityFrameworkCore;
using ReportingSystem.API.Repository.IRepository;
using ReportingSystem.API.Repository;

namespace ReportingSystem.API.Middleware
{
    public static class Registrar
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ILoginRepository, LoginRepository>()
                .AddScoped<ILoginService, LoginService>()
                .AddScoped<IPatientReportService, PatientReportService>()
                .AddScoped<IMailService, MailService>()
                .AddScoped<IExcelReader, ExcelReader>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }

        public static IServiceCollection RegisterDataServices(this IServiceCollection services)
        {
            //var DefaultConnection = "workstation id=mssql-88450-0.cloudclusters.net,12597;TrustServerCertificate=true;user id=LaBeachUser;pwd=Gr8@54321;data source=mssql-88450-0.cloudclusters.net,12597;persist security info=False;initial catalog=KaashiYatri";
            var DefaultConnection = "workstation id=mssql-88450-0.cloudclusters.net,12597;TrustServerCertificate=true;user id=ReportingSystem;pwd=Gr8@12345;data source=mssql-88450-0.cloudclusters.net,12597;persist security info=False;initial catalog=ReportingSystem";
            services.AddDbContext<ReportingSystemContext>(
                options => { options.UseSqlServer(DefaultConnection); }
            );
            var serviceProvider = services.BuildServiceProvider();
            //ApplyMigrations(serviceProvider);
            return services;
        }


        public static void ApplyMigrations(this IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetRequiredService<ReportingSystemContext>();
            //db.Database.Migrate();
        }
    }
}