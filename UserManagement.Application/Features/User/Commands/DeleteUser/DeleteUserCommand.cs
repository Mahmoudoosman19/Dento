using Common.Application.Abstractions.Messaging;

namespace UserManagement.Application.Features.User.Commands.DeleteUser
{
    public class DeleteUserCommand : ICommand
    {
        public Guid userId { get; set; }
    }
}
