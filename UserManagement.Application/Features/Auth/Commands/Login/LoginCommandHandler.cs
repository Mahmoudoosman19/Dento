using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using MapsterMapper;
using UserManagement.Application.Abstractions;
using UserManagement.Application.DTOs;
using UserManagement.Application.Identity;

namespace UserManagement.Application.Features.Auth.Commands.Login
{
    internal class LoginCommandHandler : ICommandHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly CustomUserManager _userManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mapper;

        public LoginCommandHandler(CustomUserManager userManager, IJwtProvider jwtProvider, IMapper mapper)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
            _mapper = mapper;
        }
        public async Task<ResponseModel<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var jwtToken = await _jwtProvider.Generate(user!);

            return new LoginCommandResponse()
            {
                Token = jwtToken,
                User = _mapper.Map<UserDto>(user!),
            };
        }
    }
}
