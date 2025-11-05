using UserManagement.Domain.Entities;

namespace UserManagement.Application.Abstractions
{
    public interface IEmailService
    {
        Task SendWelcomeEmailAsync(User user, CancellationToken cancellationToken = default);

        void SendEmail(string email, string subject, string body);

    }
}
