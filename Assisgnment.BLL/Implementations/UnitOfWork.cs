using Assisgnment.BLL.Interfaces;
using Assisgnment.DAL.Data;
using System;
using System.Threading.Tasks;

namespace Assisgnment.BLL.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly Lazy<IDoctorRepository> _doctorRepository;
        private readonly Lazy<IPatientRepository> _patientRepository;
        private readonly Lazy<IAppointmentRepository> _appointmentRepository;

        public IDoctorRepository Doctors => _doctorRepository.Value;
        public IPatientRepository Patients => _patientRepository.Value;
        public IAppointmentRepository Appointments => _appointmentRepository.Value;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _doctorRepository = new Lazy<IDoctorRepository>(() => new DoctorRepository(_dbContext));
            _patientRepository = new Lazy<IPatientRepository>(() => new PatientRepository(_dbContext));
            _appointmentRepository = new Lazy<IAppointmentRepository>(() => new AppointmentRepository(_dbContext));
        }

        public async Task<int> SaveChangesAsync()
            => await _dbContext.SaveChangesAsync();
    }
}
