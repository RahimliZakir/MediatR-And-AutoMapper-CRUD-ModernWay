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
                using (IServiceScope scope = ctx.ActionContext.HttpContext.RequestServices.CreateScope())
                {
                    // 2nd. Way For Prevent Eager Loading
                    //TransportDbContext db = scope.ServiceProvider.GetService<TransportDbContext>();
                }

                if (request.Id == null || request.Id <= 0)
                {
                    return 0;
                }

                Passenger entity = await db.Passengers.FirstOrDefaultAsync(p => p.Id.Equals(request.Id)
                                         , cancellationToken);

                // 1st. Way For Prevent Eager Loading
                //Passenger entity = await db.Passengers.AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(request.Id)
                //                         , cancellationToken);

                if (entity == null)
                {
                    ctx.AddModelError("", "Belə bir məlumat mövcud deyil!");
                    return 0;
                }

                if (ctx.IsValid())
                {
                    // Other Way
                    //request.CreatedDate = entity.CreatedDate;

                    request.GeneratedSecretKey = entity.GeneratedSecretKey;
                    request.CreatedByUserId = entity.CreatedByUserId;
                    Passenger passenger = mapper.Map(request, entity);

                    // Way For Prevent Eager Loading
                    //db.Passengers.Update(passenger);
                    await db.SaveChangesAsync(cancellationToken);

                    return entity.Id;
                }

                return 0;
            }
        }
    }
}
