using MediatR;
using MediatRAndAutoMapper.WebUI.AppCode.Infrastructure;
using MediatRAndAutoMapper.WebUI.Models.DataContexts;
using MediatRAndAutoMapper.WebUI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatRAndAutoMapper.WebUI.AppCode.Modules.PassengersModule
{
    public class PassengereRemoveCommand : IRequest<JsonCommandResponse>
    {
        public int? Id { get; set; }

        public class PassengereRemoveCommandHandler : IRequestHandler<PassengereRemoveCommand, JsonCommandResponse>
        {
            readonly TransportDbContext db;

            public PassengereRemoveCommandHandler(TransportDbContext db)
            {
                this.db = db;
            }


            async public Task<JsonCommandResponse> Handle(PassengereRemoveCommand request, CancellationToken cancellationToken)
            {
                JsonCommandResponse response = new JsonCommandResponse();

                if (request.Id == null || request.Id <= 0)
                {
                    response.Error = true;
                    response.Message = "Məlumat tamlığı qorunmayıb!";
                    goto end;
                }

                Passenger entity = await db.Passengers.FirstOrDefaultAsync(a => a.Id.Equals(request.Id), cancellationToken);

                if (entity == null)
                {
                    response.Error = true;
                    response.Message = "Belə məlumat yoxudr!";
                    goto end;
                }

                response.Error = false;
                response.Message = "Seçdiyiniz məlumat uğurla silindi!";

                db.Passengers.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);

            end:
                return response;
            }
        }
    }
}
