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
                //using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtpout.secureserver.net"))
                //{
                //    client.Port = 587;
                //    client.EnableSsl = false;
                //    client.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);

                //    MailMessage mail = new MailMessage();
                //    mail.From = new MailAddress(_mailSettings.Mail);
                //    mail.To.Add(mailRequest.ToEmail); // Replace with the recipient's email address
                //    mail.Subject = mailRequest.Subject;
                //    mail.IsBodyHtml = true;
                //    mail.Body = mailRequest.Body;

                //    client.Send(mail);
                //}
                // Create an SMTP client
                //System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient("182.50.151.48"); // Replace with your SMTP server

                //// Set SMTP credentials
                //smtpClient.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password); // Replace with your username and password

                //// Enable SSL if required
                //smtpClient.EnableSsl = true; // Use SSL/TLS if required

                //// Create a MailMessage
                //MailMessage mail = new MailMessage();
                //mail.From = new MailAddress(_mailSettings.Mail); // Replace with your Gmail address
                //mail.To.Add(mailRequest.ToEmail); // Replace with the recipient's email address
                //mail.Subject = "Test Email";
                //mail.Body = "This is a test email from C# Web API.";

                //// Send the email
                //smtpClient.Send(mail);

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_mailSettings.Mail));
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = mailRequest.Subject;

                var builder = new BodyBuilder
                {
                    HtmlBody = mailRequest.Body,
                };

                email.Body = builder.ToMessageBody();

                using var client = new MailKit.Net.Smtp.SmtpClient();
                //client.Connect(_mailSettings.GmailHost, _mailSettings.GmailPort, false);
                client.Connect(_mailSettings.GoDaddyHost, _mailSettings.GoDaddyPort, false);
                client.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                client.Send(email);
                client.Disconnect(true);
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
