using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using MapsterMapper;
using UserManagement.Application.DTOs;
using UserManagement.Application.Specifications.Customer;
using UserManagement.Domain.Entites;

namespace UserManagement.Application.Features.Customer.Queries.GetAddressByCustomerId
{
    public class GetListAddressQueryHandler : IQueryHandler<GetAddressByCustomerIdQuery, IEnumerable<CustomerAddressQueryResponse>>
    {
        private readonly IGenericRepository<Address> _AddressRepo;
        private readonly IMapper _Mapper;
        private readonly ITokenExtractor _tokenExtractor;

        public GetListAddressQueryHandler(IMapper mapper, IGenericRepository<Address> addressRepo, ITokenExtractor tokenExtractor)
        {
            _Mapper = mapper;
            _AddressRepo = addressRepo;
            _tokenExtractor = tokenExtractor;
        }
        public async Task<ResponseModel<IEnumerable<CustomerAddressQueryResponse>>> Handle(GetAddressByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _tokenExtractor.GetUserId();
            if(userId == null)
            {
                return ResponseModel.Failure<IEnumerable<CustomerAddressQueryResponse>>(Messages.UserNotFound);

            }
            var (listQuery, count) = _AddressRepo.GetWithSpec(new ListCustomerAddressSpecification(userId) );
            if (listQuery == null)
            { 
                return ResponseModel.Failure <IEnumerable<CustomerAddressQueryResponse>> (Messages.NotFoundAddresstoUser);
            }
            var addressData=_Mapper.Map <IEnumerable<CustomerAddressQueryResponse>> (listQuery);
            return ResponseModel.Success(addressData, count);
        }
    }
}
