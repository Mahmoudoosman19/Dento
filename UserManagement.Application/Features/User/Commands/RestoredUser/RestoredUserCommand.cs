using Common.Application.Abstractions.Messaging;

namespace UserManagement.Application.Features.User.Commands.RestoredUser
{
    public class RestoredUserCommand : ICommand
    {
        public Guid userId { get; set; }
    }

}
