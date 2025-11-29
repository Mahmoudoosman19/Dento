using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Presentation.Configurations;
using UserManagement.API.OptionsSetup;
using UserManagement.Application;
using UserManagement.Application.Abstractions;
using UserManagement.Domain.Options;
using UserManagement.Infrastructure;
using UserManagement.Infrastructure.Seeders;
using UserManagement.Presentation.Extensions;
using UserManagement.Presentation.OptionsSetup;
using UserManagement.Service;

namespace UserManagement.Presentation
{
    public static class PresentationDependencies
    {
        public static IServiceCollection AddUserPresentationStrapping(this IServiceCollection services,IConfiguration configuration)
        {
            services.ConfigureOptions<JwtOptionsSetup>();
            services.ConfigureOptions<OTPOptionsSetup>();


            // Layers Dependencies 
            services.AddUserApplicationStrapping(); //Application Layer
            services.AddUserInfrastructureStrapping(); //Infrastructure Layer
            services.AddUserServiceStrapping(configuration); //Service Layer

            // Other Extensions and services
            services.AddUserServicesDIConfig();
            services.AddDBSeederExtension();
            services.AddUserIdentitySevice(configuration);
            return services;
        }
    }
}
