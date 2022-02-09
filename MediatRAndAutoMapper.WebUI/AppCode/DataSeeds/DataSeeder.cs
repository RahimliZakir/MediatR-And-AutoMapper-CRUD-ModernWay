using MediatRAndAutoMapper.WebUI.Models.DataContexts;
using MediatRAndAutoMapper.WebUI.Models.Entities;

namespace MediatRAndAutoMapper.WebUI.AppCode.DataSeeds
{
    public static class DataSeeder
    {
        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                TransportDbContext db = scope.ServiceProvider.GetRequiredService<TransportDbContext>();

                if (!db.Passengers.Any())
                {
                    db.Passengers.Add(new Passenger
                    {
                        Name = "Zakir",
                        Surname = "Rahimli",
                        Age = 19,
                        TicketNumber = "123"
                    });

                    db.SaveChanges();
                }
            }

            return app;
        }
    }
}
