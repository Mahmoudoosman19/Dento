using FluentValidation;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.Customer.Commands.CustomerAddAddress
{
    public class AddAddressValidator : AbstractValidator<AddAddressCommand>
    {
        public AddAddressValidator()
        {
            ValidationRules();
        }
        private void ValidationRules()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(Messages.EmptyField);
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage(Messages.EmptyField)
                .Must(IsPhoneNumberValid).WithMessage(Messages.IncorrectData);
            RuleFor(x => x.AddressName)
                .NotEmpty().WithMessage(Messages.EmptyField);
            RuleFor(x => x.City)
                .NotEmpty().WithMessage(Messages.EmptyField);
        }
        private bool IsPhoneNumberValid(string phoneNumber)
        {
            return !string.IsNullOrEmpty(phoneNumber) &&
                   phoneNumber.All(char.IsDigit) &&
                   phoneNumber.Length == 11;
        }
    }
}
