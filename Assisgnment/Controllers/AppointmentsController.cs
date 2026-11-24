using AutoMapper;
using Assisgnment.BLL.Interfaces;
using Assisgnment.DAL.Models;
using Assisgnment.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assisgnment.Controllers
{
    public class AppointmentsController : Controller
    {
        private const int PageSize = 10;
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;

        public AppointmentsController(
            IAppointmentService appointmentService,
            IDoctorService doctorService,
            IPatientService patientService,
            IMapper mapper)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _patientService = patientService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchValue, int pageIndex = 1)
        {
            var result = await _appointmentService.GetAllAsync(searchValue, pageIndex, PageSize);
            if (!result.Successeded || result.Data is null)
                return Problem(result.Error);

            var appointmentVMs = _mapper.Map<IEnumerable<AppointmentViewModel>>(result.Data);
            var paginatedVM = BLL.Models.PaginatedList<AppointmentViewModel>.Create(appointmentVMs, result.Data.TotalCount, result.Data.PageIndex, PageSize);

            return View(paginatedVM);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDoctorsAndPatientsViewBagAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentViewModel appointmentVM)
        {
            if (!ModelState.IsValid)
            {
                await LoadDoctorsAndPatientsViewBagAsync();
                return View(appointmentVM);
            }

            var appointment = _mapper.Map<Appointment>(appointmentVM);
            var result = await _appointmentService.CreateAsync(appointment);

            if (!result.Successeded)
            {
                ModelState.AddModelError(string.Empty, result.Error ?? "An error occurred while creating appointment.");
                await LoadDoctorsAndPatientsViewBagAsync();
                return View(appointmentVM);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
            => await AppointmentControllerHandlerAsync(id, nameof(Details));

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
            => await AppointmentControllerHandlerAsync(id, nameof(Edit));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, AppointmentViewModel appointmentVM)
        {
            if (id != appointmentVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var appointment = _mapper.Map<Appointment>(appointmentVM);
                var result = await _appointmentService.UpdateAsync(appointment);

                if (!result.Successeded)
                {
                    ModelState.AddModelError(string.Empty, result.Error ?? "An error occurred while updating appointment.");
                    await LoadDoctorsAndPatientsViewBagAsync();
                    return View(appointmentVM);
                }

                return RedirectToAction(nameof(Index));
            }

            await LoadDoctorsAndPatientsViewBagAsync();
            return View(appointmentVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
            => await AppointmentControllerHandlerAsync(id, nameof(Delete));

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var result = await _appointmentService.DeleteAsync(id.Value);

            if (!result.Successeded)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> AppointmentControllerHandlerAsync(int? id, string viewName)
        {
            if (!id.HasValue)
                return BadRequest();

            var result = await _appointmentService.GetByIdAsync(id.Value);
            if (!result.Successeded || result.Data is null)
                return NotFound();

            if (viewName == nameof(Edit))
            {
                await LoadDoctorsAndPatientsViewBagAsync();
            }

            var appointmentVM = _mapper.Map<AppointmentViewModel>(result.Data);
            return View(viewName, appointmentVM);
        }

        private async Task LoadDoctorsAndPatientsViewBagAsync()
        {
            var doctorsResult = await _doctorService.GetAllAsync(null, 1, int.MaxValue);
            var patientsResult = await _patientService.GetAllAsync(null, 1, int.MaxValue);

            var doctors = doctorsResult.Data ?? Enumerable.Empty<Doctor>();
            var patients = patientsResult.Data ?? Enumerable.Empty<Patient>();

            ViewBag.Doctors = new SelectList(doctors, nameof(Doctor.Id), nameof(Doctor.Name));
            ViewBag.Patients = new SelectList(patients, nameof(Patient.Id), nameof(Patient.Name));
        }
    }
}
