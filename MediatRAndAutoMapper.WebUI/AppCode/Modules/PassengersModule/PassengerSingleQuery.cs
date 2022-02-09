using MediatR;
using MediatRAndAutoMapper.WebUI.Models.DataContexts;
using MediatRAndAutoMapper.WebUI.Models.Entities;

namespace MediatRAndAutoMapper.WebUI.AppCode.Modules.PassengersModule
{
    public class PassengerSingleQuery : IRequest<Passenger>
    {
        public int Id { get; set; }

        public class PassengerSingleQueryHandler : IRequestHandler<PassengerSingleQuery, Passenger>
        {
            readonly TransportDbContext db;

            public PassengerSingleQueryHandler(TransportDbContext db)
            {
                this.db = db;
            }

            async public Task<Passenger> Handle(PassengerSingleQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
