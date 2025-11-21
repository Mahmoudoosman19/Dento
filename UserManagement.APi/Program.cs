using Case.Application;
using Case.Infrastructure;
using Case.Presentation;
using IdentityHelper.BI;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UserManagement.API.Configurations;
using UserManagement.API.OptionsSetup;
using UserManagement.Application;
using UserManagement.Infrastructure;
using UserManagement.Infrastructure.Data;
using UserManagement.Infrastructure.Seeders;
using UserManagement.Presentation;
using UserManagement.Presentation.Extensions;
using UserManagement.Presentation.OptionsSetup;

namespace DentalDesign.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Common Services
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();


            // Add Case Strapping
            builder.Services.AddCasePresentationStrapping(builder.Configuration);

            // Add User Strapping
            builder.Services.AddUserPresentationStrapping(builder.Configuration);

            // Identity & Authentication Services
            builder.Services.AddIdentityHelper();


            builder.Services.AddControllers()
                .AddApplicationPart(UserManagement.Presentation.AssemblyReference.Assembly)
                .AddApplicationPart(Case.Presentation.AssemblyReference.Assembly);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            #region Configure Kestrel Middlewares

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion


            #region Update Database
            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;

            var userContext = service.GetRequiredService<UserDbContext>();

            var loggerFactory = service.GetRequiredService<ILoggerFactory>();
            try
            {
                await userContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occured while updating database!");
            } 
            #endregion

            app.Run();
        }
    }
}
