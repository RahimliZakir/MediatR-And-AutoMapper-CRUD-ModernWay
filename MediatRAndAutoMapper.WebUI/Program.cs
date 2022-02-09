using MediatR;
using MediatRAndAutoMapper.WebUI.Models.DataContexts;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfiguration conf = builder.Configuration;

IServiceCollection services = builder.Services;
services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

services.AddControllersWithViews();

services.AddDbContext<TransportDbContext>(options =>
{
    options.UseSqlServer(conf.GetConnectionString("cString"));
});

services.AddMediatR(typeof(Program));
services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

services.AddAutoMapper(typeof(Program));

WebApplication app = builder.Build();
IWebHostEnvironment env = builder.Environment;
if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "{controller=Passengers}/{action=Index}/{id?}");
});

app.Run();
