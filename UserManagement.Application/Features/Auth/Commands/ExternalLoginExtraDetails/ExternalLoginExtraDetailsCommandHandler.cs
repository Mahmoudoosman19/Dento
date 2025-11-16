using Common.Application.Abstractions.Messaging;
using Common.Domain.Repositories;
using Common.Domain.Shared;
using IdentityHelper.Abstraction;
using MapsterMapper;
using UserManagement.Application.Abstractions;
using UserManagement.Application.DTOs;
using UserManagement.Application.Features.Auth.Commands.Login;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Auth.Commands.ExternalLoginExtraDetails;

public class ExternalLoginExtraDetailsCommandHandler : ICommandHandler<ExternalLoginExtraDetailsCommand, LoginCommandResponse>
{
    private readonly CustomUserManager _userManager;
    private readonly IGenericRepository<Domain.Entities.User> _userRepo;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;
    private readonly ITokenExtractor _currentUser;

    public ExternalLoginExtraDetailsCommandHandler(CustomUserManager userManager, IGenericRepository<Domain.Entities.User> userRepo, IJwtProvider jwtProvider, IMapper mapper, ITokenExtractor currentUser)
    {
        _userManager = userManager;
        _userRepo = userRepo;
        _jwtProvider = jwtProvider;
        _mapper = mapper;
        _currentUser = currentUser;
    }
    
    public async Task<ResponseModel<LoginCommandResponse>> Handle(ExternalLoginExtraDetailsCommand request, CancellationToken cancellationToken)
    {
        var email = _currentUser.GetEmail();
        var user = await _userManager.FindByEmailAsync(email);
        
        user = UpdateUserData(user!, request);
        
        _userRepo.Update(user);

        await _userRepo.SaveChangesAsync(cancellationToken);

        var token = await _jwtProvider.Generate(user);
        
        return new LoginCommandResponse()
        {
            Token = token,
            User = _mapper.Map<UserDto>(user!)
        };
    }

    private Domain.Entities.User UpdateUserData(Domain.Entities.User user, ExternalLoginExtraDetailsCommand request)
    {
        user!.SetGender(request.Gender);
        
        user!.SetStatus(UserStatus.Active);
        
        user!.SetBirthDate(request.BirthDate);
        
        user.SetPhoneNumber(request.PhoneNumber);

        user.UserName = request.UserName;
        
        user.NormalizedUserName = user.UserName!.ToUpper();

        return user;
    }
}