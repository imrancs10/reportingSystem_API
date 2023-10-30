using ReportingSystem.API.Config;
using ReportingSystem.API.DTO.Response;
using ReportingSystem.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ReportingSystem.API.Data
{
    public class ReportingSystemContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        private readonly string _defaultConnection;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReportingSystemContext(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            //_defaultConnection = "workstation id=mssql-88450-0.cloudclusters.net,12597;TrustServerCertificate=true;user id=LaBeachUser;pwd=Gr8@54321;data source=mssql-88450-0.cloudclusters.net,12597;persist security info=False;initial catalog=KaashiYatri";
            _defaultConnection = "workstation id=mssql-88450-0.cloudclusters.net,12597;TrustServerCertificate=true;user id=LaBeachUser;pwd=Gr8@54321;data source=mssql-88450-0.cloudclusters.net,12597;persist security info=False;initial catalog=ReportingSystem";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            options.UseSqlServer(_defaultConnection);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<PatientReport> PatientReports { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        public override int SaveChanges()
        {
            AddDateTimeStamp();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddDateTimeStamp();
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private void AddDateTimeStamp()
        {
            foreach (var entityEntry in ChangeTracker.Entries())
            {
                var hasChange = entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified;
                if (!hasChange) continue;
                if (!(entityEntry.Entity is BaseModel baseModel)) continue;
                var now = DateTime.Now;
                int? userId = null;
                if ((bool)_httpContextAccessor.HttpContext?.Request.Headers.ContainsKey("userId"))
                {
                    string value = _httpContextAccessor.HttpContext?.Request.Headers["userId"].ToString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        //var userData = Users.FirstOrDefault(x => x.UserName == value && x.Role == valueRole);
                        //if (userData != null)
                        //{
                        //    userId = userData.Id;
                        //}
                        if (int.TryParse(value, out int newUserId))
                        {
                            userId = newUserId;
                        }
                    }
                }
                if (entityEntry.State is EntityState.Added)
                {
                    baseModel.CreatedBy = userId != null ? userId.Value : 0;
                    baseModel.CreatedAt = now;
                }
                else
                {
                    baseModel.UpdatedBy = userId != null ? userId.Value : 0;
                    entityEntry.Property("CreatedAt").IsModified = false;
                    baseModel.UpdatedAt = now;
                }
            }
        }
    }
}
