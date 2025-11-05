using UserManagement.Application.Abstractions;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Services
{
    internal class SMSService : ISMSService
    {
        public Task SendConfirmationSMSAsync(User user, CancellationToken cancellationToken = default)
            => Task.CompletedTask;
    }
}
