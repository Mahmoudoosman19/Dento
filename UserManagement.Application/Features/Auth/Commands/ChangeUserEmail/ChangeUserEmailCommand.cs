using Common.Application.Abstractions.Messaging;

namespace UserManagement.Application.Features.Auth.Commands.ChangeUserEmail
{
    public class ChangeUserEmailCommand : ICommand
    {
        public string Email { get; set; }
    }
}
