using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using UserManagement.Application.Identity;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.Auth.Commands.ChangePassword
{
    internal class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
    {
        private readonly CustomUserManager _userManager;
        private readonly ITokenExtractor _tokenExtractor;
        public ChangePasswordCommandHandler(CustomUserManager userManager, ITokenExtractor tokenExtractor)
        {
            _userManager = userManager;
            _tokenExtractor = tokenExtractor;
        }
        public async Task<ResponseModel> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = _tokenExtractor.GetUserId();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            user!.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.NewPassword);

            if (request.CurrentPassword == request.NewPassword)
                return ResponseModel.Failure(Messages.Thenewpasswordcannotbethesameastheoldpassword_);


            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return ResponseModel.Failure(result.Errors.FirstOrDefault()!.Description);
            return ResponseModel.Success(Messages.SuccessChangePassword);


        }
    }
}
