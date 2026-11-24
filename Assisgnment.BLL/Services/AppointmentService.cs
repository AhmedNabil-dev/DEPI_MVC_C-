using Assisgnment.BLL.Interfaces;
using Assisgnment.BLL.Models;
using Assisgnment.DAL.Models;
using System.Threading.Tasks;

namespace Assisgnment.BLL.Services
{
    public sealed class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Appointment>> CreateAsync(Appointment appointment)
        {
            await _unitOfWork.Appointments.CreateAsync(appointment);
            await _unitOfWork.SaveChangesAsync();
            return Result<Appointment>.Success(appointment);
        }

        public async Task<Result<Appointment>> DeleteAsync(int id)
        {
            var result = await GetByIdAsync(id);
            if (!result.Successeded || result.Data is null)
                return result;

            _unitOfWork.Appointments.Delete(result.Data);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Result<PaginatedList<Appointment>>> GetAllAsync(string? searchValue, int pageIndex, int pageSize)
        {
            var appointments = await _unitOfWork.Appointments.GetAllWithDetailsAsync(searchValue);
            var paginatedList = PaginatedList<Appointment>.Create(appointments, pageIndex, pageSize);
            return Result<PaginatedList<Appointment>>.Success(paginatedList);
        }

        public async Task<Result<Appointment>> GetByIdAsync(int id)
        {
            var appointment = await _unitOfWork.Appointments.GetWithDetailsAsync(id);
            return appointment is null
                ? Result<Appointment>.Failure("Appointment not found.")
                : Result<Appointment>.Success(appointment);
        }

        public async Task<Result<Appointment>> UpdateAsync(Appointment appointment)
        {
            _unitOfWork.Appointments.Update(appointment);
            await _unitOfWork.SaveChangesAsync();
            return Result<Appointment>.Success(appointment);
        }
    }
}
