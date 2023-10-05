using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using ReportingSystem.API.Constants;
using ReportingSystem.API.Dto;
using ReportingSystem.API.Dto.Request;
using ReportingSystem.API.Services.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace ReportingSystem.API.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task<string> GetMailTemplete(EmailTemplateEnum emailTemplateEnum)
        {
            string content = string.Empty;
            var pathToFile = Directory.GetCurrentDirectory()
                                 + Path.DirectorySeparatorChar.ToString()
                                 + "EmailTemplate"
                                 + Path.DirectorySeparatorChar.ToString()
                                 + $"{emailTemplateEnum}.html";
            using (StreamReader SourceReader = File.OpenText(pathToFile))
            {
                content = await SourceReader.ReadToEndAsync();
            }
            return content;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                var email = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(_mailSettings.Mail)
                };

                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = mailRequest.Subject;
                var builder = new BodyBuilder();
                if (mailRequest.Attachments != null)
                {
                    byte[] fileBytes;
                    foreach (var file in mailRequest.Attachments)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms); fileBytes = ms.ToArray();
                            }
                            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                        }
                    }
                }
                builder.HtmlBody = mailRequest.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
