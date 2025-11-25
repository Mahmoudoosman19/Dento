using Common.Application.Behaviors;
using System.Net.NetworkInformation;
using UserManagement.Application;
using UserManagement.Presentation;
using IdentityHelper.BI;


namespace DentalDesign.Dashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Common Services
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();

            // Add User Strapping
            builder.Services.AddUserPresentationStrapping(builder.Configuration);

            // Identity & Authentication Services
            builder.Services.AddIdentityHelper();


            // Add services to the container.
            builder.Services.AddControllersWithViews();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
