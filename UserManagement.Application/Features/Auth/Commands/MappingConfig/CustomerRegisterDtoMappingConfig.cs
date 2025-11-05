using Common.Application.Extensions.String;
using Mapster;
using UserManagement.Application.Features.Auth.Commands.Register.DTOs;

namespace UserManagement.Application.Features.Auth.Commands.MappingConfig
{
    internal class CustomerRegisterDtoMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CustomerRegisterDto, Domain.Entities.User>()
                .Map(dest => dest.FullNameAr, src => src.FullName.IsArabicLanguage() ? src.FullName : null)
                .Map(dest => dest.FullNameEn, src => src.FullName.IsEnglishLanguage() ? src.FullName : null);
        }
    }
}
