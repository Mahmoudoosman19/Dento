using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Abstractions;

namespace UserManagement.Infrastructure.Seeders
{
    public static class DBSeederExtension
    {
        public static async Task<IServiceCollection> AddDBSeederExtension(this IServiceCollection services)
        {
            services.AddScoped<ISeeder, RolesSeeder>();
            services.AddScoped<ISeeder, PermissionsSeeder>();
            services.AddScoped<ISeeder, RolesPermissionsSeeder>();
            services.AddScoped<ISeeder, UsersSeeder>();
            services.AddScoped<ISeeder, DesignerSeeder>();

            using var serviceProvider = services.BuildServiceProvider();

            var seeders = serviceProvider.GetServices<ISeeder>();

            seeders = seeders.OrderBy(x => x.ExecutionOrder);

            foreach (var seeder in seeders)
                await seeder.SeedAsync();

            return services;
        }
    }
}
