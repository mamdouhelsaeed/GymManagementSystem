using GymManagementSystem.Filters;
using GymManagementSystem.Models;
using GymManagementSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementSystem.Controllers
{
    [AdminOnly]
    public class GymClassesController : Controller
    {
        private readonly IGymClassRepository _gymClassRepository;
        private readonly ITrainerRepository _trainerRepository;

        public GymClassesController(IGymClassRepository gymClassRepository, ITrainerRepository trainerRepository)
        {
            _gymClassRepository = gymClassRepository;
            _trainerRepository = trainerRepository;
        }

        private async Task PopulateTrainersDropDown(int? selectedId = null)
        {
            var trainers = await _trainerRepository.GetAllAsync();
            ViewBag.TrainerId = new SelectList(trainers, "Id", "Name", selectedId);
        }

        // GET: /GymClasses
        public async Task<IActionResult> Index()
        {
            var classes = await _gymClassRepository.GetAllAsync();
            return View(classes);
        }

        // GET: /GymClasses/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var gymClass = await _gymClassRepository.GetByIdWithDetailsAsync(id);
            if (gymClass == null) return NotFound();
            return View(gymClass);
        }

        // GET: /GymClasses/Create
        public async Task<IActionResult> Create()
        {
            await PopulateTrainersDropDown();
            return View();
        }

        // POST: /GymClasses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Schedule,TrainerId")] GymClass gymClass)
        {
            if (!ModelState.IsValid)
            {
                await PopulateTrainersDropDown(gymClass.TrainerId);
                return View(gymClass);
            }

            await _gymClassRepository.AddAsync(gymClass);
            await _gymClassRepository.SaveChangesAsync();
            TempData["Success"] = "Class created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /GymClasses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var gymClass = await _gymClassRepository.GetByIdAsync(id);
            if (gymClass == null) return NotFound();
            await PopulateTrainersDropDown(gymClass.TrainerId);
            return View(gymClass);
        }

        // POST: /GymClasses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Schedule,TrainerId")] GymClass gymClass)
        {
            if (id != gymClass.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                await PopulateTrainersDropDown(gymClass.TrainerId);
                return View(gymClass);
            }

            _gymClassRepository.Update(gymClass);
            await _gymClassRepository.SaveChangesAsync();
            TempData["Success"] = "Class updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /GymClasses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var gymClass = await _gymClassRepository.GetByIdWithDetailsAsync(id);
            if (gymClass == null) return NotFound();
            return View(gymClass);
        }

        // POST: /GymClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _gymClassRepository.DeleteAsync(id);
            await _gymClassRepository.SaveChangesAsync();
            TempData["Success"] = "Class deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
