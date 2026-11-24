using AutoMapper;
using Assisgnment.BLL.Interfaces;
using Assisgnment.DAL.Models;
using Assisgnment.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assisgnment.Controllers
{
    public class PatientsController : Controller
    {
        private const int PageSize = 10;
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;

        public PatientsController(IPatientService patientService, IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchValue, int pageIndex = 1)
        {
            var result = await _patientService.GetAllAsync(searchValue, pageIndex, PageSize);
            if (!result.Successeded || result.Data is null)
                return Problem(result.Error);

            var patientVMs = _mapper.Map<IEnumerable<PatientViewModel>>(result.Data);
            var paginatedVM = BLL.Models.PaginatedList<PatientViewModel>.Create(patientVMs, result.Data.TotalCount, result.Data.PageIndex, PageSize);

            return View(paginatedVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientViewModel patientVM)
        {
            if (!ModelState.IsValid)
                return View(patientVM);

            var patient = _mapper.Map<Patient>(patientVM);
            var result = await _patientService.CreateAsync(patient);

            if (!result.Successeded)
            {
                ModelState.AddModelError(string.Empty, result.Error ?? "An error occurred while creating patient.");
                return View(patientVM);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
            => await PatientControllerHandlerAsync(id, nameof(Details));

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
            => await PatientControllerHandlerAsync(id, nameof(Edit));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, PatientViewModel patientVM)
        {
            if (id != patientVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var patient = _mapper.Map<Patient>(patientVM);
                var result = await _patientService.UpdateAsync(patient);

                if (!result.Successeded)
                {
                    ModelState.AddModelError(string.Empty, result.Error ?? "An error occurred while updating patient.");
                    return View(patientVM);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(patientVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
            => await PatientControllerHandlerAsync(id, nameof(Delete));

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var result = await _patientService.DeleteAsync(id.Value);

            if (!result.Successeded)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> PatientControllerHandlerAsync(int? id, string viewName)
        {
            if (!id.HasValue)
                return BadRequest();

            var result = await _patientService.GetByIdAsync(id.Value);
            if (!result.Successeded || result.Data is null)
                return NotFound();

            var patientVM = _mapper.Map<PatientViewModel>(result.Data);
            return View(viewName, patientVM);
        }
    }
}
