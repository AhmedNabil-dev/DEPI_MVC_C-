using Assisgnment.BLL.Interfaces;
using Assisgnment.BLL.Models;
using Assisgnment.DAL.Models;
using System.Threading.Tasks;

namespace Assisgnment.BLL.Services
{
    public sealed class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Patient>> CreateAsync(Patient patient)
        {
            await _unitOfWork.Patients.CreateAsync(patient);
            await _unitOfWork.SaveChangesAsync();
            return Result<Patient>.Success(patient);
        }

        public async Task<Result<Patient>> DeleteAsync(int id)
        {
            var result = await GetByIdAsync(id);
            if (!result.Successeded || result.Data is null)
                return result;

            _unitOfWork.Patients.Delete(result.Data);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Result<PaginatedList<Patient>>> GetAllAsync(string? searchValue, int pageIndex, int pageSize)
        {
            var patients = string.IsNullOrWhiteSpace(searchValue)
                ? await _unitOfWork.Patients.GetAllAsync()
                : await _unitOfWork.Patients.GetAllAsync(searchValue);

            var paginatedList = PaginatedList<Patient>.Create(patients, pageIndex, pageSize);
            return Result<PaginatedList<Patient>>.Success(paginatedList);
        }

        public async Task<Result<Patient>> GetByIdAsync(int id)
        {
            var patient = await _unitOfWork.Patients.GetAsync(id);
            return patient is null
                ? Result<Patient>.Failure("Patient not found.")
                : Result<Patient>.Success(patient);
        }

        public async Task<Result<Patient>> GetWithAppointmentsAsync(int id)
        {
            var patient = await _unitOfWork.Patients.GetWithAppointmentsAsync(id);
            return patient is null
                ? Result<Patient>.Failure("Patient not found.")
                : Result<Patient>.Success(patient);
        }

        public async Task<Result<Patient>> UpdateAsync(Patient patient)
        {
            _unitOfWork.Patients.Update(patient);
            await _unitOfWork.SaveChangesAsync();
            return Result<Patient>.Success(patient);
        }
    }
}
