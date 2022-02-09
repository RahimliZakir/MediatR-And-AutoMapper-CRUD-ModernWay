using MediatRAndAutoMapper.WebUI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatRAndAutoMapper.WebUI.Models.DataContexts
{
    public class TransportDbContext : DbContext
    {
        public TransportDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Passenger> Passengers { get; set; }
    }
}
