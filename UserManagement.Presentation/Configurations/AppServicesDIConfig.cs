using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace UserManagement.API.Configurations
{
    public static class AppServicesDIConfig
    {
        public static IServiceCollection AddAppServicesDIConfig(this IServiceCollection services)
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
