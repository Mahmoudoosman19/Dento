using Case.Domain.Entities;
using Case.Domain.Repositories;
using Case.Infrastructure.Data;
using Case.Infrastructure.Repositories;
using Common.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Infrastructure
{
    public static class Bootstrap
    {
        public static IServiceCollection AddCaseInfrastructureStrapping(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>),typeof(Case.Infrastructure.Repositories.CaseGenericRepository<>));
            services.AddScoped<ICaseUnitOfWork, CaseUnitOfWork>();
            return services;
        }
    }
}
