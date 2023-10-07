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
using System.Net.Mail;
using System.Net;
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
            try
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "EmailTemplate", $"{emailTemplateEnum}.html");
                //var pathToFile = Directory.GetCurrentDirectory()
                //                     + Path.DirectorySeparatorChar.ToString()
                //                     + "EmailTemplate"
                //                     + Path.DirectorySeparatorChar.ToString()
                //                     + $"{emailTemplateEnum}.html";
                using (StreamReader SourceReader = File.OpenText(basePath))
                {
                    content = await SourceReader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
            }
            return content;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com"))
                {
                    client.Port = 587;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(_mailSettings.Mail);
                    mail.To.Add(mailRequest.ToEmail); // Replace with the recipient's email address
                    mail.Subject = mailRequest.Subject;
                    mail.IsBodyHtml = true;
                    mail.Body = mailRequest.Body;

                    client.Send(mail);
                }

                //var email = new MimeMessage
                //{
                //    Sender = MailboxAddress.Parse(_mailSettings.Mail)
                //};

                //email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                //email.Subject = mailRequest.Subject;
                //var builder = new BodyBuilder();
                //if (mailRequest.Attachments != null)
                //{
                //    byte[] fileBytes;
                //    foreach (var file in mailRequest.Attachments)
                //    {
                //        if (file.Length > 0)
                //        {
                //            using (var ms = new MemoryStream())
                //            {
                //                file.CopyTo(ms); fileBytes = ms.ToArray();
                //            }
                //            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                //        }
                //    }
                //}
                //builder.HtmlBody = mailRequest.Body;
                //email.Body = builder.ToMessageBody();
                //using var smtp = new SmtpClient();
                //smtp.Connect(_mailSettings.Host, _mailSettings.Port);
                //smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                //await smtp.SendAsync(email);
                //smtp.Disconnect(true);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
