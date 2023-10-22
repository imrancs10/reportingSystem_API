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
using System.Linq.Expressions;
using DocumentFormat.OpenXml.Wordprocessing;
using SmtpClient = System.Net.Mail.SmtpClient;

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
                SmtpClient client = new SmtpClient("relay-hosting.secureserver.net", 25);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("support@techrefreshsolution.com", "ADMV@12345");
                MailMessage message = new MailMessage("support@techrefreshsolution.com", mailRequest.ToEmail);
                message.Subject = mailRequest.Subject;
                message.Body = mailRequest.Body;
                message.IsBodyHtml = true;
                client.Send(message);

                //var email = new MimeMessage();
                //email.From.Add(MailboxAddress.Parse(_mailSettings.Mail));
                //email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                //email.Subject = mailRequest.Subject;

                //var builder = new BodyBuilder
                //{
                //    HtmlBody = mailRequest.Body,
                //};

                //email.Body = builder.ToMessageBody();

                //using var client = new MailKit.Net.Smtp.SmtpClient();
                ////client.Connect(_mailSettings.GmailHost, _mailSettings.GmailPort, false);
                //client.Connect(_mailSettings.GoDaddyHost, _mailSettings.GoDaddyPort, false);
                //client.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                //client.Send(email);
                //client.Disconnect(true);
            }
            catch (Exception ex)
            {
                throw ex;
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
    }
}
