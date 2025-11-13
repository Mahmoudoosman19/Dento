
using Mapster;
using UserManagement.Application.Features.Customer.Queries.GetVendorInformation;

namespace UserManagement.Application.Features.Customer.Queries.MappingConfig
{
    public class GetDesignerInformationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Domain.Entities.Designer, GetInformationResponse>()
              .Map(dest => dest.FullNameAr, src => src.User.FullNameAr)
              .Map(dest => dest.FullNameEn, src => src.User.FullNameEn )
              .Map(dest => dest.UserName, src => src.User.UserName );

        }
    } 
}
    
