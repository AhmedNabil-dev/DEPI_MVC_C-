using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assisgnment.BLL.Implementations
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync(string search)
        {
            var lowerSearch = search.ToLower();
            return await _dbSet
                .Where(d => d.Name.ToLower().Contains(lowerSearch) || d.Specialization.ToLower().Contains(lowerSearch))
                .ToListAsync();
        }

        public async Task<Doctor?> GetWithAppointmentsAsync(int id)
        {
            return await _dbSet
                .Include(d => d.Appointments)
                    .ThenInclude(a => a.Patient)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
