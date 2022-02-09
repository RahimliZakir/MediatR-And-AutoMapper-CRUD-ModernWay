using AutoMapper;
using MediatR;
using MediatRAndAutoMapper.WebUI.AppCode.Extensions;
using MediatRAndAutoMapper.WebUI.Models.DataContexts;
using MediatRAndAutoMapper.WebUI.Models.Entities;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MediatRAndAutoMapper.WebUI.AppCode.Modules.PassengersModule
{
    public class PassengerEditCommand : PassengerViewModel, IRequest<int>
    {
        public class PassengerEditCommandHandler : IRequestHandler<PassengerEditCommand, int>
        {
            readonly TransportDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IMapper mapper;

            public PassengerEditCommandHandler(TransportDbContext db, IActionContextAccessor ctx, IMapper mapper)
            {
                this.db = db;
                this.ctx = ctx;
                this.mapper = mapper;
            }

            async public Task<int> Handle(PassengerEditCommand request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.Id <= 0)
                {
                    return 0;
                }

                Passenger entity = await db.Passengers.FirstOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);

                if (entity == null)
                {
                    ctx.AddModelError("", "Belə bir məlumat mövcud deyil!");
                    return 0;
                }

                Passenger passenger = mapper.Map<Passenger>(request);

                if (ctx.IsValid())
                {
                    db.Passengers.Update(passenger);
                    await db.SaveChangesAsync(cancellationToken);

                    return entity.Id;
                }

                return 0;
            }
        }
    }
}
