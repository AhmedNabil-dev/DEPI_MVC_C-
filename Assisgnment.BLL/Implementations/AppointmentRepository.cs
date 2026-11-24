using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assisgnment.BLL.Implementations
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Appointment>> GetAllWithDetailsAsync(string? search = null)
        {
            var query = _dbSet
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var lowerSearch = search.ToLower();
                query = query.Where(a =>
                    (a.Doctor != null && a.Doctor.Name.ToLower().Contains(lowerSearch)) ||
                    (a.Patient != null && a.Patient.Name.ToLower().Contains(lowerSearch)) ||
                    a.Status.ToLower().Contains(lowerSearch));
            }

            return await query.ToListAsync();
        }

        public async Task<Appointment?> GetWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
