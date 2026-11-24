using AutoMapper;
using Assisgnment.DAL.Models;
using Assisgnment.ViewModels;

namespace Assisgnment.Helper
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientViewModel>().ReverseMap();
        }
    }
}
