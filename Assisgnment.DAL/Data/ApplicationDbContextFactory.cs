using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Assisgnment.DAL.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=DEPI_Hospital_MVC;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
