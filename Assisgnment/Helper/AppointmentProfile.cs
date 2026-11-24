using AutoMapper;
using Assisgnment.DAL.Models;
using Assisgnment.ViewModels;

namespace Assisgnment.Helper
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentViewModel>().ReverseMap();
        }
    }
}
