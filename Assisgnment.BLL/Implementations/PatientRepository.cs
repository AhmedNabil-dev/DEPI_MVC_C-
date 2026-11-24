using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assisgnment.BLL.Implementations
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Patient>> GetAllAsync(string search)
        {
            var lowerSearch = search.ToLower();
            return await _dbSet
                .Where(p => p.Name.ToLower().Contains(lowerSearch) || (p.Phone != null && p.Phone.Contains(search)))
                .ToListAsync();
        }

        public async Task<Patient?> GetWithAppointmentsAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Appointments)
                    .ThenInclude(a => a.Doctor)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
