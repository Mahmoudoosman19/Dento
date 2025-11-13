using Common.Application.Extensions.FluentValidation;
using Common.Domain.Repositories;
using UserManagement.Domain.Entites;

namespace UserManagement.Application.Features.Customer.Commands.CustomerDeletedAddressById
{
    public class DeleteAddressByIdValidator : AbstractValidator<DeleteAddressByIdCommand>
    {
        public DeleteAddressByIdValidator(IGenericRepository<Address> addressRepo)
        {
            RuleFor(x => x.Id)
               .NotEmpty()
               .WithMessage(Messages.EmptyField)
               .EntityExist(addressRepo)
               .WithMessage(Messages.NotFound);
        }
    }
}
