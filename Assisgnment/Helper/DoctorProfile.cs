using AutoMapper;
using Assisgnment.DAL.Models;
using Assisgnment.ViewModels;

namespace Assisgnment.Helper
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, DoctorViewModel>().ReverseMap();
        }
    }
}
