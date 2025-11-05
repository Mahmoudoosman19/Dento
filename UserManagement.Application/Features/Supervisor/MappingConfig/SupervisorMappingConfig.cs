using Mapster;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Features.Supervisor.MappingConfig
{
    internal class SupervisorMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Domain.Entities.Supervisor, SupervisorDto>()
                .Map(dest => dest.UserId, src => src.User.Id)
                .Map(dest => dest.UserName, src => src.User.UserName)
                .Map(dest => dest.Email, src => src.User.Email)
                .Map(dest => dest.PhoneNumber, src => src.User.PhoneNumber)
                .Map(dest => dest.FullNameAr, src => src.User.FullNameAr)
                .Map(dest => dest.FullNameEn, src => src.User.FullNameEn)
                .Map(dest => dest.Gender, src => src.User.Gender)
                .Map(dest => dest.Status, src => src.User.Status)
                .Map(dest => dest.PhoneNumber, src => src.User.PhoneNumber);
        }
    }
}
