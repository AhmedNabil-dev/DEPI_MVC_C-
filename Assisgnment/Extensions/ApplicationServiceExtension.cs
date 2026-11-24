using Assisgnment.BLL.Implementations;
using Assisgnment.BLL.Interfaces;
using Assisgnment.BLL.Services;
using Assisgnment.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Assisgnment.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IAppointmentService, AppointmentService>();

            services.AddAutoMapper(config =>
                config.AddMaps(Assembly.GetEntryAssembly())
            );

            return services;
        }
    }
}
