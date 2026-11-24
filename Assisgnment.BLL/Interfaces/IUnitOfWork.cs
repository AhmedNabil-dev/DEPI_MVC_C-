using System.Threading.Tasks;

namespace Assisgnment.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        IDoctorRepository Doctors { get; }
        IPatientRepository Patients { get; }
        IAppointmentRepository Appointments { get; }
        Task<int> SaveChangesAsync();
    }
}
