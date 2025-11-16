using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Features.Auth.Commands.ExternalLogin.Abstract;

namespace UserManagement.Application.Features.Auth.Commands.ExternalLogin.Types;

public static class ExternalLoginBootstrap
{
    public static IServiceCollection RegisterExternalLoginTypes(this IServiceCollection services)
    {
        services.AddScoped<BaseExternalLogin, GoogleLoginType>();
        services.AddScoped<BaseExternalLogin, FacebookLoginType>();

        return services;
    }
}