using Case.Presentation;
using Common.Application.Behaviors;
using DentalDesign.Dashboard.Helper;
using DentalDesign.Dashboard.Middleware;
using IdentityHelper.BI;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using UserManagement.Application;
using UserManagement.Presentation;


namespace DentalDesign.Dashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            // Common Services
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();

            // Add Case Strapping
            builder.Services.AddCasePresentationStrapping(builder.Configuration);

            // Add User Strapping
            builder.Services.AddUserPresentationStrapping(builder.Configuration);


            // Identity & Authentication Services
            builder.Services.AddIdentityHelper();


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Authorization
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("DynamicActionPermission", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.AddRequirements(new DynamicActionPermissionRequirement());
                });
            });
            builder.Services.AddScoped<IAuthorizationHandler, DynamicActionPermissionHandler>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseMiddleware<TokenForwardingMiddleware>();
            app.UseMiddleware<ActionPermissionMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
