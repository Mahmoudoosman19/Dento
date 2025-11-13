using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using UserManagement.Application.Specifications.Customer;
using UserManagement.Domain.Entites;
using UserManagement.Domain.Resources;

namespace UserManagement.Application.Features.Customer.Commands.CustomerAddAddress
{
    public class AddAddressCommandHandler : ICommandHandler<AddAddressCommand>
    {
        private readonly IGenericRepository<Address> _addressRepo;
        private readonly ITokenExtractor _tokenExtractor;
        public AddAddressCommandHandler(IGenericRepository<Address> addressRepo, ITokenExtractor tokenExtractor)
        {
            _addressRepo = addressRepo;
            _tokenExtractor = tokenExtractor;
        }
        public async Task<ResponseModel> Handle(AddAddressCommand request, CancellationToken cancellationToken)
        {
            var address = new Address();

            var userId = _tokenExtractor.GetUserId();
            var data = _addressRepo.GetWithSpec(new AddAddressCustomerSpecification(request, userId));
            if (data.count != 0)
            {
                return ResponseModel.Failure(Messages.ThisAddressAlreadyExciting);
            }
            else
            {
                address.setData(request.Name,
                    request.AddressName, request.City,
                    request.PhoneNumber, request.Floor,
                userId);
            }
            await _addressRepo.AddAsync(address);
            await _addressRepo.SaveChangesAsync();
            return ResponseModel.Success(Messages.SuccessfulOperation);

        }
    }
}
