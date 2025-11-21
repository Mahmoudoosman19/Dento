using Common.Domain.Shared;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Abstractions
{
    public interface IEmailService
    {
        Task SendWelcomeEmailAsync(User user, CancellationToken cancellationToken = default);

        Task<ResponseModel> SendEmail(string email, string subject, string body);

    }
}
