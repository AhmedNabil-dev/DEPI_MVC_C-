using Assisgnment.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assisgnment.BLL.Interfaces
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<IEnumerable<Patient>> GetAllAsync(string search);
        Task<Patient?> GetWithAppointmentsAsync(int id);
    }
}
