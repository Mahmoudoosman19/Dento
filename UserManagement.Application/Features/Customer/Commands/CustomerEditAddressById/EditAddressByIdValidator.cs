namespace UserManagement.Application.Features.Customer.Commands.CustomerEditAddressById
{
    public class EditAddressByIdValidator : AbstractValidator<EditAddressByIdCommand>
    {
        public EditAddressByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(Messages.EmptyField);
        }
    }
}
