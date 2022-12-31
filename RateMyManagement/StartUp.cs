using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Radzen;
using RateMyManagement.Areas.Identity;
using RateMyManagement.Core.EntityFramework;
using RateMyManagement.Core.EntityFramework.Authorization;
using RateMyManagement.IServices;
using RateMyManagement.Services;

namespace RateMyManagement;

public class StartUp
{
    public static void ConfigureService(IServiceCollection services, string dbConnectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(dbConnectionString);
            options.EnableSensitiveDataLogging();
        });
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        services.AddAuthorizationCore(AuthorizationOptionsConfigurer.Configure);
        services.AddRazorPages();
        services.AddServerSideBlazor();


        services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();
        services.AddScoped<ICompanyService, SqlCompanyService>();
        services.AddScoped<ILocationService, SqlLocationService>();
        services.AddSingleton<IImageService, ImgbbService>();
        services.AddSingleton<IAuthorizationHandler, LocationManagerHandler>();
        services.AddSingleton<IAuthorizationHandler, CompanyManagerHandler>();
        services.AddScoped<ContextMenuService>();
    }

    public static void ConfigureApplication(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");
    }
}