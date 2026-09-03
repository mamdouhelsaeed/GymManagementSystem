using GymManagementSystem.Filters;
using GymManagementSystem.Models;
using GymManagementSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers
{
    [AdminOnly] // Server-side enforcement: only Admins may reach these actions
    public class TrainersController : Controller
    {
        private readonly ITrainerRepository _trainerRepository;

        public TrainersController(ITrainerRepository trainerRepository)
        {
            _trainerRepository = trainerRepository;
        }

        // GET: /Trainers
        public async Task<IActionResult> Index()
        {
            var trainers = await _trainerRepository.GetAllAsync();
            return View(trainers);
        }

        // GET: /Trainers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var trainer = await _trainerRepository.GetByIdWithClassesAsync(id);
            if (trainer == null) return NotFound();
            return View(trainer);
        }

        // GET: /Trainers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Trainers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Specialization")] Trainer trainer)
        {
            if (!ModelState.IsValid) return View(trainer);

            await _trainerRepository.AddAsync(trainer);
            await _trainerRepository.SaveChangesAsync();
            TempData["Success"] = "Trainer created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Trainers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var trainer = await _trainerRepository.GetByIdAsync(id);
            if (trainer == null) return NotFound();
            return View(trainer);
        }

        // POST: /Trainers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Specialization")] Trainer trainer)
        {
            if (id != trainer.Id) return NotFound();
            if (!ModelState.IsValid) return View(trainer);

            _trainerRepository.Update(trainer);
            await _trainerRepository.SaveChangesAsync();
            TempData["Success"] = "Trainer updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Trainers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var trainer = await _trainerRepository.GetByIdWithClassesAsync(id);
            if (trainer == null) return NotFound();
            return View(trainer);
        }

        // POST: /Trainers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _trainerRepository.DeleteAsync(id);
            await _trainerRepository.SaveChangesAsync();
            TempData["Success"] = "Trainer deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
