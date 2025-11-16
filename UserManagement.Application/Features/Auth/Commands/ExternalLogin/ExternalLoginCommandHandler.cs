using Common.Application.Abstractions.Messaging;
using Common.Domain.Shared;
using UserManagement.Application.Features.Auth.Commands.ExternalLogin.Abstract;
using UserManagement.Application.Features.Auth.Commands.Login;

namespace UserManagement.Application.Features.Auth.Commands.ExternalLogin;

internal class ExternalLoginCommandHandler : ICommandHandler<ExternalLoginCommand, LoginCommandResponse>
{
    private readonly IExternalLoginFactory _externalLoginFactory;
    public ExternalLoginCommandHandler(IExternalLoginFactory externalLoginFactory)
    {
        _externalLoginFactory = externalLoginFactory;
    }
    
    public async Task<ResponseModel<LoginCommandResponse>> Handle(ExternalLoginCommand request, CancellationToken cancellationToken)
    {
        var externalLogin = _externalLoginFactory.Login(request.LoginProvider);

        var result = await externalLogin.Login(request);

        return result;
    }
}