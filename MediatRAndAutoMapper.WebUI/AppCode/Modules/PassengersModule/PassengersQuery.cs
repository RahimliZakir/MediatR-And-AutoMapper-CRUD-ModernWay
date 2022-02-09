using MediatR;
using MediatRAndAutoMapper.WebUI.Models.DataContexts;
using MediatRAndAutoMapper.WebUI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatRAndAutoMapper.WebUI.AppCode.Modules.PassengersModule
{
    public class PassengersQuery : IRequest<IEnumerable<Passenger>>
    {
        public class PassengersQueryHandler : IRequestHandler<PassengersQuery, IEnumerable<Passenger>>
        {
            readonly TransportDbContext db;

            public PassengersQueryHandler(TransportDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<Passenger>> Handle(PassengersQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Passenger> passengers = await db.Passengers.ToListAsync(cancellationToken);

                return passengers;
            }
        }
    }
}
