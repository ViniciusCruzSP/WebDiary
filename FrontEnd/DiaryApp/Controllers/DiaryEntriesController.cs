using DiaryApp.Exceptions;
using DiaryApp.Models.DTO;
using DiaryApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Threading.Tasks;

namespace DiaryApp.Controllers
{
    public class DiaryEntriesController : Controller
    {
        private readonly DiaryApiService _service;
        public DiaryEntriesController(DiaryApiService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Diary()
        {
            var entries = await _service.GetAllAsync();
            return View(entries);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DiaryEntryDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            try
            {
                await _service.CreateAsync(dto);
                return RedirectToAction("Diary");
            }
            catch (ApiValidationException ex)
            {
                foreach (var error in ex.ProblemDetails.Errors)
                {
                    foreach (var message in error.Value)
                    {
                        ModelState.AddModelError(error.Key, message);
                    }
                }
                return View(dto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var entry = await _service.GetByIdAsync(id);
                return View(entry);
            }
            catch (ApiNotFoundException)
            {
                return RedirectToAction(nameof(Diary));
            }

            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, DiaryEntryDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            try
            {
                await _service.UpdateAsync(id, dto);
                return RedirectToAction("Diary");
            }
            catch (ApiValidationException ex)
            {
                foreach (var error in ex.ProblemDetails.Errors)
                {
                    foreach (var message in error.Value)
                    {
                        ModelState.AddModelError(error.Key, message);
                    }
                }
                return View(dto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("Diary");
        }

    }
}
