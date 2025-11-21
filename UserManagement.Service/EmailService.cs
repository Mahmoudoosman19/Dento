using Common.Domain.Shared;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;
using UserManagement.Application.Abstractions;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Options;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace UserManagement.Service
{
    internal class EmailService : IEmailService
    {
        private readonly SmtpOptions _options;
        private readonly ILogger<EmailService> _logger;
        private readonly IConfiguration _config;

        public EmailService( ILogger<EmailService> logger,IConfiguration config)
        {
            _config = config;
            _logger = logger;
            _options = config.GetSection("Smtp").Get<SmtpOptions>()!;
        }

        public Task SendWelcomeEmailAsync(User user, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public async Task<ResponseModel> SendEmail(string email, string subject, string body)
        {
            try
            {
                using(var client= new SmtpClient())
                {
                    await client.ConnectAsync(_options.Host, _options.Port, _options.UseSsl);
                    client.Authenticate(_options.UserName,_options.Password);

                    var bodyBuilder = new BodyBuilder()
                    {
                        HtmlBody = $"{body}",
                        TextBody = body
                    };
                    var message = new MimeMessage()
                    {
                        Body = bodyBuilder.ToMessageBody()
                    };

                    message.From.Add(new MailboxAddress(_options.FromName, _options.FromAddress));
                    message.To.Add(new MailboxAddress("Testing", email));
                    message.Subject = subject;

                    await client.SendAsync(message);
                    client.Disconnect(true);
                }

                return ResponseModel.Success();
            }catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {email}", email);
                return ResponseModel.Failure($"Failed to send email to {email}");
            }


            
            ////try
            //{
            //    using (var smtpClient = new SmtpClient(_host, _port))
            //    {

            //        smtpClient.Credentials = new NetworkCredential(_userName, _key);
            //        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //        smtpClient.EnableSsl = true;
            //        smtpClient.UseDefaultCredentials = false;
            //        var mail = new MailMessage
            //        {
            //            From = new MailAddress(_userName),
            //            Subject = subject,
            //            Body = body,
            //            IsBodyHtml = false,
            //        };

            //        mail.To.Add(email);

            //        smtpClient.Send(mail);

            //        _logger.LogInformation($"Email successfully sent to {email}");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError($"Error sending email to {email}: {ex.Message}");
            //    throw;
            //}
        }
    }
}
