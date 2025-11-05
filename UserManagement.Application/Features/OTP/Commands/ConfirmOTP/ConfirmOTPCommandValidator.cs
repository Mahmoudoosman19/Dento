using Common.Domain.Repositories;
using FluentValidation;
using UserManagement.Application.Identity;
using UserManagement.Application.Specifications.OTP;
using UserManagement.Domain.Enums;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.OTP.Commands.ConfirmOTP
{
    internal class ConfirmOTPCommandValidator : AbstractValidator<ConfirmOTPCommand>
    {
        private readonly CustomUserManager _userManager;
        private readonly IGenericRepository<Domain.Entities.OTP> _oTPRepo;
        private Domain.Entities.OTP? _otp;

        public ConfirmOTPCommandValidator(CustomUserManager userManager, IGenericRepository<Domain.Entities.OTP> OTPRepo)
        {
            _userManager = userManager;
            _oTPRepo = OTPRepo;
            
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .Must(OTPExistAsync).WithMessage(Messages.NotFound);
            
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .EmailAddress().WithMessage(Messages.InvalidEmailAddress)
                .MustAsync(((email, token) => _userManager.IsUserExistByEmailAsync(email)))
                .WithMessage(Messages.NotFound);

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage(Messages.IncorrectData)
                .Must(OTPTypeMatches).WithMessage(Messages.IncorrectData);
        }
        private async Task<bool> UserExistAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _userManager.IsUserExistByIdAsync(id);
        }

        private bool OTPExistAsync(string code)
        {
            GetOTP(code);

            if (_otp is null)
                return false;

            if (_otp.IsUsed || _otp.ExpireOn < DateTime.UtcNow)
                return false;
            return true;
        }

        private bool OTPTypeMatches(ConfirmOTPCommand command, OTPType type)
        {
            GetOTP(command.Code);

            if (_otp is null)
                return false;

            return _otp.Type == type;
        }

        private void GetOTP(string code)
        {
            if (_otp is null)
                _otp = _oTPRepo.GetEntityWithSpec(new GetOTPByCodeSpecification(code));
        }
    }
}
