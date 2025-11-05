using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using UserManagement.Application.Abstractions;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Options;

namespace UserManagement.Infrastructure.Services
{
    internal class EmailService : IEmailService
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _userName;
        private readonly string _key;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<SmtpOptions> smtpOptions, ILogger<EmailService> logger)
        {
            _host = smtpOptions.Value.Host;
            _port = smtpOptions.Value.Port;
            _userName = smtpOptions.Value.UserName;
            _key = smtpOptions.Value.Key;
            _logger = logger;
        }
        public Task SendWelcomeEmailAsync(User user, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public void SendEmail(string email, string subject, string body)
        {
            try
            {
                using (var smtpClient = new SmtpClient(_host, _port))
                {

                    smtpClient.Credentials = new NetworkCredential(_userName, _key);
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    var mail = new MailMessage
                    {
                        From = new MailAddress(_userName),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = false,
                    };

                    mail.To.Add(email);

                    smtpClient.Send(mail);

                    _logger.LogInformation($"Email successfully sent to {email}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending email to {email}: {ex.Message}");
                throw;
            }
        }
    }
}
