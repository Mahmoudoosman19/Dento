using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using UserManagement.Application.Specifications.Customer;
using UserManagement.Domain.Entites;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.Customer.Commands.CustomerDeletedAddressById
{
    public class DeleteAddressByIdCommandHandler : ICommandHandler<DeleteAddressByIdCommand>
    {
        private readonly IGenericRepository<Address> _addressRepo;
        private readonly ITokenExtractor _tokenExtractor;

        public DeleteAddressByIdCommandHandler(IGenericRepository<Address> addressRepo, ITokenExtractor tokenExtractor)
        {
            _addressRepo = addressRepo;
            _tokenExtractor = tokenExtractor;
        }
        public async Task<ResponseModel> Handle(DeleteAddressByIdCommand request, CancellationToken cancellationToken)
        {
            var userId = _tokenExtractor.GetUserId();
            var address = _addressRepo.GetEntityWithSpec(new DeleteAddressSpecification(request, userId));
            if (address == null)
            {
                return ResponseModel.Failure(Messages.NotAccessDeleteThisAddress);
            }
            _addressRepo.Delete(address);
            await _addressRepo.SaveChangesAsync();
            return ResponseModel.Success(Messages.SuccessfulOperation);
        }
    }
}
