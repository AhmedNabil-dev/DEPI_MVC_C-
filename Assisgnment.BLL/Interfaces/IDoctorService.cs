using Assisgnment.BLL.Models;
using Assisgnment.DAL.Models;
using System.Threading.Tasks;

namespace Assisgnment.BLL.Interfaces
{
    public interface IDoctorService
    {
        Task<Result<PaginatedList<Doctor>>> GetAllAsync(string? searchValue, int pageIndex, int pageSize);
        Task<Result<Doctor>> GetByIdAsync(int id);
        Task<Result<Doctor>> GetWithAppointmentsAsync(int id);
        Task<Result<Doctor>> CreateAsync(Doctor doctor);
        Task<Result<Doctor>> UpdateAsync(Doctor doctor);
        Task<Result<Doctor>> DeleteAsync(int id);
    }
}
