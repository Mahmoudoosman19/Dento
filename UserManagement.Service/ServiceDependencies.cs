using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Abstractions;
using UserManagement.Domain.Options;

namespace UserManagement.Service
{
    public static class ServiceDependencies
    {
        public static IServiceCollection AddUserServiceStrapping(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SmtpOptions>(configuration.GetSection("Smtp"));
            services.AddSingleton<IEmailService, EmailService>();

            return services;
        }
    }
}
