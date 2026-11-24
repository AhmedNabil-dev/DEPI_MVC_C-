using Assisgnment.BLL.Models;
using Assisgnment.DAL.Models;
using System.Threading.Tasks;

namespace Assisgnment.BLL.Interfaces
{
    public interface IAppointmentService
    {
        Task<Result<PaginatedList<Appointment>>> GetAllAsync(string? searchValue, int pageIndex, int pageSize);
        Task<Result<Appointment>> GetByIdAsync(int id);
        Task<Result<Appointment>> CreateAsync(Appointment appointment);
        Task<Result<Appointment>> UpdateAsync(Appointment appointment);
        Task<Result<Appointment>> DeleteAsync(int id);
    }
}
