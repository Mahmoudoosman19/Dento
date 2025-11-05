using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using UserManagement.Application.Features.Auth.Commands.Register.Abstract;

namespace UserManagement.Application.Features.Auth.Commands.Register
{
    internal class RegisterCommandHandler : ICommandHandler<RegisterCommand>
    {
        private readonly IRegisterFactory _registerFactory;

        public RegisterCommandHandler(IRegisterFactory registerFactory)
        {
            _registerFactory = registerFactory;
        }
        public async Task<ResponseModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var register = _registerFactory.Register(request.Type);

            return await register.Register(request);
        }
    }
}
