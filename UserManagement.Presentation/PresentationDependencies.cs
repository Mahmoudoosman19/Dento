using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Presentation.OptionsSetup;

namespace UserManagement.Presentation
{
    public static class PresentationDependencies
    {
        public static IServiceCollection AddUserPresentationStrapping(this IServiceCollection services)
        {
            services.ConfigureOptions<JwtOptionsSetup>();
            return services;
        }
    }
}
