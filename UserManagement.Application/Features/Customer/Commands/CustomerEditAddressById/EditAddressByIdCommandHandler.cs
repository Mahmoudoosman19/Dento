using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using UserManagement.Domain.Entites;

namespace UserManagement.Application.Features.Customer.Commands.CustomerEditAddressById
{
    public class EditAddressByIdCommandHandler : ICommandHandler<EditAddressByIdCommand>
    {
        private readonly IGenericRepository<Address> _addressRepo;
        private readonly ITokenExtractor _tokenExtractor;

        public EditAddressByIdCommandHandler(IGenericRepository<Address> addressRepo, ITokenExtractor tokenExtractor)
        {
            _addressRepo = addressRepo;
            _tokenExtractor = tokenExtractor;
        }
        public async Task<ResponseModel> Handle(EditAddressByIdCommand request, CancellationToken cancellationToken)
        {
            var address = await _addressRepo.GetByIdAsync(request.Id);
            if (address == null)
            {
                return ResponseModel.Failure(Messages.AddressNotfound);
            }
            var userId = _tokenExtractor.GetUserId();
            if (userId == null)
            {
                return ResponseModel.Failure(Messages.UserNotFound);

            }
            if (userId != address.UserId)
            {
                return ResponseModel.Failure(Messages.NotAccessEditthisAddress);
            }
            else
            {
                address.setData(
                    request.Name,
                    request.AddressName
                    , request.City,
                    request.PhoneNumber,
                    request.Floor, userId
                );
            }
            _addressRepo.Update(address);
            await _addressRepo.SaveChangesAsync();
            return ResponseModel.Success(Messages.SuccessfulOperation);

        }
    }
}
