using Assisgnment.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assisgnment.BLL.Interfaces
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAllWithDetailsAsync(string? search = null);
        Task<Appointment?> GetWithDetailsAsync(int id);
    }
}
