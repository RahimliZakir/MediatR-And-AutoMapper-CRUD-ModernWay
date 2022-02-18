using MediatR;
using MediatRAndAutoMapper.WebUI.AppCode.DataSeeds;
using MediatRAndAutoMapper.WebUI.Models.DataContexts;
using MediatRAndAutoMapper.WebUI.Models.Entities.Membership;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfiguration conf = builder.Configuration;

IServiceCollection services = builder.Services;
services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

services.AddControllersWithViews(cfg =>
{
    AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                                     .RequireAuthenticatedUser()
                                     .Build();

    cfg.Filters.Add(new AuthorizeFilter(policy));
});

services.AddDbContext<TransportDbContext>(options =>
{
    options.UseSqlServer(conf.GetConnectionString("cString"));
});

services.AddMediatR(typeof(Program));
services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

services.AddAutoMapper(typeof(Program));

services.AddIdentity<AppUser, AppRole>()
        .AddEntityFrameworkStores<TransportDbContext>()
        .AddDefaultTokenProviders();

services.AddScoped<UserManager<AppUser>>()
        .AddScoped<RoleManager<AppRole>>()
        .AddScoped<SignInManager<AppUser>>();

services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
    options.User.RequireUniqueEmail = true;
});

services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(13);

    options.LoginPath = "/signin.html";
    options.AccessDeniedPath = "/accessdenied.html";
    options.SlidingExpiration = true;

    options.Cookie.Name = ".Transport.Cookie.Analysers";

    options.Cookie.SameSite = SameSiteMode.Strict;
});

WebApplication app = builder.Build();
IWebHostEnvironment env = builder.Environment;
if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.SeedData();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("SignInRoute", "signin.html",
        defaults: new
        {
            action = "SignIn",
            controller = "Account"
        });

    endpoints.MapControllerRoute("RegisterRoute", "register.html",
        defaults: new
        {
            action = "Register",
            controller = "Account"
        });

    endpoints.MapControllerRoute("AccessDeniedRoute", "accessdenied.html",
        defaults: new
        {
            action = "AccessDenied",
            controller = "Account"
        });

    endpoints.MapControllerRoute("default", "{controller=Passengers}/{action=Index}/{id?}");
});

app.Run();
