using Assisgnment.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assisgnment.BLL.Interfaces
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        Task<IEnumerable<Doctor>> GetAllAsync(string search);
        Task<Doctor?> GetWithAppointmentsAsync(int id);
    }
}
