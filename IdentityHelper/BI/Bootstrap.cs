using IdentityHelper.Abstraction;
using IdentityHelper.Service;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityHelper.BI
{
    public static class Bootstrap
    {
        public static void AddIdentityHelper(this IServiceCollection services)
        {
            services.AddScoped<ITokenExtractor, TokenExtractor>();
            services.AddScoped<IUserManagement, UserManagement>();
        }
    }
}
