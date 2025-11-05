using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using UserManagement.Application.Identity;
using UserManagement.Application.Specifications.User;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.Auth.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler : ICommandHandler<ForgotPasswordCommand>
{
    private readonly CustomUserManager _userManager;
    private readonly IUnitOfWork _uow;

    public ForgotPasswordCommandHandler(CustomUserManager userManager, IUnitOfWork uow)
    {
        _userManager = userManager;
        _uow = uow;
    }
    public async Task<ResponseModel> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user =  _uow.Repository<Domain.Entities.User>()
            .GetEntityWithSpec(new GetUserByEmailWithOtpSpecification(request.Email));
        
        _userManager.ChangePassword(user!, request.NewPassword);
        
        _uow.Repository<Domain.Entities.OTP>().Delete(user!.Otp!);
        user!.SetOtp(null);
        
        _uow.Repository<Domain.Entities.User>().Update(user);

        await _uow.CompleteAsync(cancellationToken);
        
        return ResponseModel.Success(Messages.SuccessChangePassword);
    }
}