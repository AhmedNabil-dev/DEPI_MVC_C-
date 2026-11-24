using AutoMapper;
using Assisgnment.BLL.Interfaces;
using Assisgnment.DAL.Models;
using Assisgnment.Utilities;
using Assisgnment.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assisgnment.Controllers
{
    public class DoctorsController : Controller
    {
        private const int PageSize = 10;
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;

        public DoctorsController(IDoctorService doctorService, IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchValue, int pageIndex = 1)
        {
            var result = await _doctorService.GetAllAsync(searchValue, pageIndex, PageSize);
            if (!result.Successeded || result.Data is null)
                return Problem(result.Error);

            var doctorVMs = _mapper.Map<IEnumerable<DoctorViewModel>>(result.Data);
            var paginatedVM = BLL.Models.PaginatedList<DoctorViewModel>.Create(doctorVMs, result.Data.TotalCount, result.Data.PageIndex, PageSize);

            return View(paginatedVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorViewModel doctorVM)
        {
            if (!ModelState.IsValid)
                return View(doctorVM);

            if (doctorVM.Image is not null)
            {
                doctorVM.ImageName = DocumentSettings.UploadFile(doctorVM.Image, "Images");
            }

            var doctor = _mapper.Map<Doctor>(doctorVM);
            var result = await _doctorService.CreateAsync(doctor);

            if (!result.Successeded)
            {
                ModelState.AddModelError(string.Empty, result.Error ?? "An error occurred while creating the doctor.");
                return View(doctorVM);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
            => await DoctorControllerHandlerAsync(id, nameof(Details));

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
            => await DoctorControllerHandlerAsync(id, nameof(Edit));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, DoctorViewModel doctorVM)
        {
            if (id != doctorVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                if (doctorVM.Image is not null)
                {
                    if (!string.IsNullOrEmpty(doctorVM.ImageName))
                    {
                        DocumentSettings.DeleteFile(doctorVM.ImageName, "Images");
                    }
                    doctorVM.ImageName = DocumentSettings.UploadFile(doctorVM.Image, "Images");
                }

                var doctor = _mapper.Map<Doctor>(doctorVM);
                var result = await _doctorService.UpdateAsync(doctor);

                if (!result.Successeded)
                {
                    ModelState.AddModelError(string.Empty, result.Error ?? "An error occurred while updating the doctor.");
                    return View(doctorVM);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(doctorVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
            => await DoctorControllerHandlerAsync(id, nameof(Delete));

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var result = await _doctorService.DeleteAsync(id.Value);

            if (!result.Successeded || result.Data is null)
                return NotFound();

            var doctor = result.Data;
            if (!string.IsNullOrEmpty(doctor.ImageName))
            {
                DocumentSettings.DeleteFile(doctor.ImageName, "Images");
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> DoctorControllerHandlerAsync(int? id, string viewName)
        {
            if (!id.HasValue)
                return BadRequest();

            var result = await _doctorService.GetByIdAsync(id.Value);
            if (!result.Successeded || result.Data is null)
                return NotFound();

            var doctorVM = _mapper.Map<DoctorViewModel>(result.Data);
            return View(viewName, doctorVM);
        }
    }
}
