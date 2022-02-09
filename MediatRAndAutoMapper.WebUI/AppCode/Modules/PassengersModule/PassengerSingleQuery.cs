using AutoMapper;
using MediatR;
using MediatRAndAutoMapper.WebUI.Models.DataContexts;
using MediatRAndAutoMapper.WebUI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatRAndAutoMapper.WebUI.AppCode.Modules.PassengersModule
{
    public class PassengerSingleQuery : IRequest<PassengerViewModel>
    {
        public int Id { get; set; }

        public class PassengerSingleQueryHandler : IRequestHandler<PassengerSingleQuery, PassengerViewModel>
        {
            readonly TransportDbContext db;
            readonly IMapper mapper;

            public PassengerSingleQueryHandler(TransportDbContext db, IMapper mapper)
            {
                this.db = db;
                this.mapper = mapper;
            }

            async public Task<PassengerViewModel> Handle(PassengerSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                    return null;

                Passenger passenger = await db.Passengers.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

                PassengerViewModel vm = mapper.Map<PassengerViewModel>(passenger);

                return vm;
            }
        }
    }
}
