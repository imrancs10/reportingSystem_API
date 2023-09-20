using ReportingSystem.API.Dto.Request;
using ReportingSystem.API.Constants;
using System.Threading.Tasks;

namespace ReportingSystem.API.Services.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task<string> GetMailTemplete(EmailTemplateEnum emailTemplateEnum);
    }
}
