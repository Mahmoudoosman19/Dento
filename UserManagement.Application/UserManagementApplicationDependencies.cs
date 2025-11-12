using Common.Application.Behaviors;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Features.Auth.Commands.Register.Abstract;
using UserManagement.Application.Features.Auth.Commands.Register.Type;
using UserManagement.Application.Features.OTP.Commands.ConfirmOTP.Abstract;
using UserManagement.Application.Features.OTP.Commands.ConfirmOTP.Type;
using UserManagement.Domain.Options;

namespace UserManagement.Application
{
    public static class UserManagementApplicationDependencies
    {
        public static IServiceCollection AddUserApplicationStrapping(this IServiceCollection services)
        {
            //mediatr
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly);

                cfg.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));

                cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            });

            services.AddValidatorsFromAssembly(
                AssemblyReference.Assembly,
                includeInternalTypes: true);

            //mapster
            services.AddMapsterConfig();

            //other services
            services.AddScoped<BaseRegister, AdminRegisterType>();
            services.AddScoped<BaseRegister, CustomerRegisterType>();
            //services.AddScoped<BaseRegister, DesignerRegisterType>();
            services.AddScoped<BaseRegister, SupervisorRegisterType>();
            //services.AddScoped<BaseRegister, VendorRegisterByAdminType>();

            services.AddScoped<BaseConfirmOTP, ConfirmUserEmailType>();
            services.AddScoped<BaseConfirmOTP, ConfirmUserPhoneType>();
            services.AddScoped<BaseConfirmOTP, ConfirmForgotPasswordType>();

           
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
