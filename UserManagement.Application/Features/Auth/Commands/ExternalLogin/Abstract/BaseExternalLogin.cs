using Common.Application.Extensions.String;
using Common.Domain.Shared;
using MapsterMapper;
using UserManagement.Application.Abstractions;
using UserManagement.Application.Features.Auth.Commands.Login;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Auth.Commands.ExternalLogin.Abstract;

internal abstract class BaseExternalLogin
{
    private readonly CustomUserManager _userManager;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;

    protected BaseExternalLogin(CustomUserManager userManager, IJwtProvider jwtProvider, IMapper mapper)
    {
        _userManager = userManager;
        _jwtProvider = jwtProvider;
        _mapper = mapper;
    }
    
    public abstract LoginProvider LoginProvider { get; set; }

    public abstract Task<ResponseModel<LoginCommandResponse>> Login(ExternalLoginCommand command);
    
    protected Domain.Entities.User CreateCustomerUser(string email, string fullname, string? appleId = null)
    {
        var user = new Domain.Entities.User()
        {
            Email = email
        };
        
        if (fullname.IsArabicLanguage())
            user.SetFullName(fullNameAr: fullname);
        else
            user.SetFullName(fullNameEn: fullname);
        
        user.SetStatus(UserStatus.MissingData);
        user.SetGender(UserGender.NotSet);
        user.AssignRole((long)Roles.Customer);
        //user.SetAppleId(appleId);

        return user;
    }
}