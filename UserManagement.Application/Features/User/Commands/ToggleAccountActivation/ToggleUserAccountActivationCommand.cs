using Common.Application.Abstractions.Messaging;

namespace UserManagement.Application.Features.User.Commands.ToggleAccountActivation
{
    public class ToggleUserAccountActivationCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
