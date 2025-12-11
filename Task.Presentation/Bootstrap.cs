using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Task.Application;
using Task.Infrastructure;
using Task.Presentation.Configurations;

namespace Task.Presentation
{
    public static class Bootstrap
    {

        public static IServiceCollection AddTaskStrapping(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddApplicationStrapping();
            services.AddInfrastructureStrapping(configuration);

            services.AddTaskServicesDIConfig();
            return services;
        }
    }
}
