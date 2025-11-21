using Case.Application;
using Case.Infrastructure;
using Case.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Case.Presentation
{
    public static class Bootstrap
    {
        public static IServiceCollection AddCasePresentationStrapping(this IServiceCollection services,IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<CaseDbContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Layers Dependencies
            services.AddCaseInfrastructureStrapping();
            services.AddCaseApplicationStrapping();

            return services;
        }
    }
}
