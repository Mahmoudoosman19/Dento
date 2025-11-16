using UserManagement.Domain.Enums;

namespace UserManagement.Application.Features.Auth.Commands.ExternalLogin.Abstract;

internal interface IExternalLoginFactory
{
    BaseExternalLogin Login(LoginProvider loginProvider);
}