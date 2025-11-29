using Common.Application.Behaviors;
using Common.Domain.Repositories;
using FluentValidation;
using Mapster;
using MapsterMapper;
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
using UserManagement.Infrastructure.Data;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Infrastructure;

public static class InfrastructureDependencies
{
    public static IServiceCollection AddUserInfrastructureStrapping(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(UserManagement.Infrastructure.Repositories.UserGenericRepository<>));
        services.AddScoped<IUnitOfWork, UserUnitOfWork>();

        return services;
    }
  
}
