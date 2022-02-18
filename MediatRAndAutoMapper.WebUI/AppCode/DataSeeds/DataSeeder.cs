using MediatRAndAutoMapper.WebUI.Models.DataContexts;
using MediatRAndAutoMapper.WebUI.Models.Entities;
using MediatRAndAutoMapper.WebUI.Models.Entities.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MediatRAndAutoMapper.WebUI.AppCode.DataSeeds
{
    public static class DataSeeder
    {
        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                TransportDbContext db = scope.ServiceProvider.GetRequiredService<TransportDbContext>();
                RoleManager<AppRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                UserManager<AppUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                db.Database.Migrate();

                //---identity---
                AppRole roleResult = roleManager.FindByNameAsync("Admin").Result;

                if (roleResult == null)
                {
                    roleResult = new AppRole
                    {
                        Name = "Admin"
                    };

                    IdentityResult roleResponse = roleManager.CreateAsync(roleResult).Result;

                    if (roleResponse.Succeeded)
                    {
                        AppUser userResult = userManager.FindByNameAsync("rahimlizakir").Result;

                        if (userResult == null)
                        {
                            userResult = new AppUser
                            {
                                UserName = "rahimlizakir",
                                Email = "zakirer@code.edu.az"
                            };

                            IdentityResult userResponse = userManager.CreateAsync(userResult, "zakir007").Result;

                            if (userResponse.Succeeded)
                            {
                                var roleUserResult = userManager.AddToRoleAsync(userResult, roleResult.Name).Result;
                            }
                        }
                        else
                        {
                            var roleUserResult = userManager.AddToRoleAsync(userResult, roleResult.Name).Result;
                        }
                    }
                }
                else
                {
                    AppUser userResult = userManager.FindByNameAsync("rahimlizakir").Result;

                    if (userResult == null)
                    {
                        userResult = new AppUser
                        {
                            UserName = "rahimlizakir",
                            Email = "zakirer@code.edu.az"
                        };

                        IdentityResult userResponse = userManager.CreateAsync(userResult, "zakir007").Result;

                        if (userResponse.Succeeded)
                        {
                            var roleUserResult = userManager.AddToRoleAsync(userResult, roleResult.Name).Result;
                        }
                    }
                    else
                    {
                        var roleUserResult = userManager.AddToRoleAsync(userResult, roleResult.Name).Result;
                    }
                }
                //---identity---

                if (!db.Passengers.Any())
                {
                    db.Passengers.Add(new Passenger
                    {
                        Name = "Zakir",
                        Surname = "Rahimli",
                        Age = 19,
                        CreatedByUserId = 1,
                        GeneratedSecretKey = Guid.NewGuid().ToString(),
                        TicketNumber = "123"
                    });

                    db.SaveChanges();
                }
            }

            return app;
        }
    }
}
