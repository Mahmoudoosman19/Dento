using IdentityHelper.Abstraction;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Auth.Commands.ExternalLoginExtraDetails;

public class ExternalLoginExtraInformationCommandValidator
    : AbstractValidator<ExternalLoginExtraDetailsCommand>
{
    private readonly CustomUserManager _userManager;
    private readonly ITokenExtractor _currentUser;
    
    public ExternalLoginExtraInformationCommandValidator(CustomUserManager userManager, ITokenExtractor currentUser)
    {
        _userManager = userManager;
        _currentUser = currentUser;


        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x)
            .MustAsync(IsMissingData).WithMessage(Messages.ExtraDataAlreadyAdded);
        
        RuleFor(x => x.Gender)
            .NotEmpty().WithMessage(Messages.EmptyField)
            .IsInEnum().WithMessage(Messages.IncorrectData);
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage(Messages.EmptyField)
            .Must(IsPhoneNumExist).WithMessage(Messages.PhoneNumberAlreadyUsed);
        
        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage(Messages.EmptyField)
            .Must(IsDateNow);
        
        
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage(Messages.EmptyField)
            .Must(IsUserNameExist).WithMessage(Messages.RedundantData);
    }
    
    private bool IsUserNameExist(string userName)
    {
        var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
        return user == null;

    }

    private bool IsPhoneNumExist(string phoneNumber)
    {
        var user = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
        return user == null;

    }
    private bool IsDateNow(DateTime date)
    {
        var now = DateTime.Now;
        if(date >= now)
        {
            return false;
        }
        return true;
    }
    
    private async Task<bool> IsEmailExist(string email, CancellationToken cancellationToken)
    {
        return await _userManager.IsUserExistByEmailAsync(email);
    }
    
    private async Task<bool> IsMissingData(ExternalLoginExtraDetailsCommand request, CancellationToken cancellationToken)
    {
        var email = _currentUser.GetEmail();
        var user = await _userManager.FindByEmailAsync(email);
        
        return user!.Status == UserStatus.MissingData;
    }
}