using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using UserManagement.Application.Identity;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.User.Commands.ToggleAccountActivation
{
    public class ToggleActivationCommandHandler : ICommandHandler<ToggleUserAccountActivationCommand>
    {
        private readonly CustomUserManager _userManager;

        public ToggleActivationCommandHandler(CustomUserManager userManager)
        {
            _userManager = userManager;
        }
        public async Task<ResponseModel> Handle(ToggleUserAccountActivationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            user!.EmailConfirmed = true;

            user!.ToggleActivation();

            await _userManager.UpdateAsync(user);

            return ResponseModel.Success(Messages.SuccessfulOperation);
        }
    }
}
