using Common.Application.Behaviors;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Task.Application
{
    public static class Bootstrap
    {
        public static IServiceCollection AddApplicationStrapping(this IServiceCollection services)
        {

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly);

                cfg.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));

                cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            });

            services.AddValidatorsFromAssembly(
                AssemblyReference.Assembly,
                includeInternalTypes: true);

            services.AddMapsterConfig();


            return services;
        }
        private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;

            config.Scan(AssemblyReference.Assembly);

            services.AddSingleton(config);

            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}
