using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace UserManagement.Presentation.Configurations
{
    public static class UserServicesDIConfig
    {
        public static IServiceCollection AddUserServicesDIConfig(this IServiceCollection services)
        {
            services
                .Scan(
                    selector => selector
                        .FromAssemblies(
                            Infrastructure.AssemblyReference.Assembly,
                            Application.AssemblyReference.Assembly,
                            Service.AssemblyReference.Assembly)
                        .AddClasses(false)
                        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

            return services;
        }
    }
}
