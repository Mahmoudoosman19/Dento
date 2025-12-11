using Common.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Task.Domain.Repository_Abstraction;
using Task.Infrastructure.Data;
using Task.Infrastructure.Repositories;

namespace Task.Infrastructure
{
    public static class Bootstrap
    {
        public static IServiceCollection AddInfrastructureStrapping(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(TaskGenericRepository<>));
            services.AddScoped<ITaskUnitOfWork, TaskUnitOfWork>();

            // DbContext
            services.AddDbContext<TaskDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );

            return services;
        }
    }
}
