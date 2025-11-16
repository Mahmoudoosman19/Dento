using UserManagement.Application.Features.Auth.Commands.ExternalLogin.Abstract;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Auth.Commands.ExternalLogin.Base;

internal class ExternalLoginFactory : IExternalLoginFactory
{
    private readonly IEnumerable<BaseExternalLogin> _externalLogins;

    public ExternalLoginFactory(IEnumerable<BaseExternalLogin> externalLogins)
    {
        _externalLogins = externalLogins;
    }

    public BaseExternalLogin Login(LoginProvider loginProvider)
    {
        var externalLogin = _externalLogins.FirstOrDefault(x => x.LoginProvider == loginProvider);

        return externalLogin!;
    }
}