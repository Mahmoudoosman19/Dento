using Common.Application.Abstractions.Messaging;

namespace UserManagement.Application.Features.Customer.Commands.CustomerDeletedAddressById
{
    public class DeleteAddressByIdCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
