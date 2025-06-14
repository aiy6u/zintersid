using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using zintersid.Data.Entities;
using zintersid.Data;
using Serilog;
using Microsoft.AspNetCore.Identity.UI.Services;
using zintersid.Services.Emailer;
using zintersid.Configurations;

namespace zintersid;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Config for Serilog
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        try
        {
            // Add services to the container.
            // Configure AppDbContext with Identity
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.RegisterProjectServices(Log.Logger);

            // Add Identity services
            builder.Services.AddIdentity<AppUser, AppRole>(options =>
                {
                    // Configure Identity options if needed
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Replace default SignInManager with AppSignInManager
            builder.Services.AddScoped<Services.Identity.AppSignInManager>();

            // Register EmailSender
            builder.Services.AddSingleton<IEmailSender, EmailSender>();

            builder.Services.AddRazorPages();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
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
            app.MapRazorPages();

            app.Run();
        }
        catch (HostAbortedException)
        {
            Console.WriteLine("HostAbortedException is ignored!");
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }

    }
}
