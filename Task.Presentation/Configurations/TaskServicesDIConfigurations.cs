using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Presentation.Configurations
{
    public static class TaskServicesDIConfigurations
    {
        public static IServiceCollection AddTaskServicesDIConfig(this IServiceCollection services)
        {
            services
                .Scan(
                    selector => selector
                        .FromAssemblies(
                            Infrastructure.AssemblyReference.Assembly,
                            Application.AssemblyReference.Assembly,
                            Presentation.AssemblyReference.Assembly)
                        .AddClasses(false)
                        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()
                );

            return services;
        }
    }
}
