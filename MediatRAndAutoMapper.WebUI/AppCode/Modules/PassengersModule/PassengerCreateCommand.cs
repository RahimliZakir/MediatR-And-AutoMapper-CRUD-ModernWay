using AutoMapper;
using MediatR;
using MediatRAndAutoMapper.WebUI.AppCode.Extensions;
using MediatRAndAutoMapper.WebUI.Models.DataContexts;
using MediatRAndAutoMapper.WebUI.Models.Entities;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MediatRAndAutoMapper.WebUI.AppCode.Modules.PassengersModule
{
    public class PassengerCreateCommand : PassengerViewModel, IRequest<int>
    {
        public class PassengerCreateCommandHandler : IRequestHandler<PassengerCreateCommand, int>
        {
            readonly TransportDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IMapper mapper;

            public PassengerCreateCommandHandler(TransportDbContext db, IActionContextAccessor ctx, IMapper mapper)
            {
                this.db = db;
                this.ctx = ctx;
                this.mapper = mapper;
            }

            async public Task<int> Handle(PassengerCreateCommand request, CancellationToken cancellationToken)
            {
                if (ctx.IsValid())
                {
                    Passenger passenger = mapper.Map<Passenger>(request);

                    await db.Passengers.AddAsync(passenger, cancellationToken);
                    await db.SaveChangesAsync(cancellationToken);

                    return passenger.Id;
                }

                return 0;
            }
        }
    }
}
