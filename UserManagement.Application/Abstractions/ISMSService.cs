using UserManagement.Domain.Entities;

namespace UserManagement.Application.Abstractions
{
    public interface ISMSService
    {
        Task SendConfirmationSMSAsync(User user, CancellationToken cancellationToken = default);
    }
}
