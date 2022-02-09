using AutoMapper;
using MediatRAndAutoMapper.WebUI.Models.Entities;

namespace MediatRAndAutoMapper.WebUI.AppCode.Modules.PassengersModule
{
    public class PassengerProfile : Profile
    {
        public PassengerProfile()
        {
            CreateMap<Passenger, PassengerViewModel>();
        }
    }
}
