using Mapster;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Entites;

namespace UserManagement.Application.Features.Customer.Queries.MappingConfig
{
    public class AddressListMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Address, CustomerAddressQueryResponse>()
              .Map(dest => dest.Address, src => src.AddressName)
              .Map(dest => dest.Floor, src => src.Floor ?? null);

        }
    }
}
