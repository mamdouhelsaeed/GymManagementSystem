using GymManagementSystem.Filters;
using GymManagementSystem.Repositories.Interfaces;
using GymManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers
{
    [LoggedInOnly]
    public class ClassesController : Controller
    {
        private readonly IGymClassRepository _gymClassRepository;
        private readonly ITrainerRepository _trainerRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public ClassesController(
            IGymClassRepository gymClassRepository,
            ITrainerRepository trainerRepository,
            IEnrollmentRepository enrollmentRepository)
        {
            _gymClassRepository = gymClassRepository;
            _trainerRepository = trainerRepository;
            _enrollmentRepository = enrollmentRepository;
        }

        // GET: /Classes
        public async Task<IActionResult> Index(int? trainerId)
        {
            var vm = new ClassesIndexViewModel
            {
                SelectedTrainerId = trainerId,
                Trainers = await _trainerRepository.GetAllAsync(),
                Classes = trainerId.HasValue
                    ? await _gymClassRepository.GetByTrainerIdAsync(trainerId.Value)
                    : await _gymClassRepository.GetAllAsync()
            };

            return View(vm);
        }

        // GET: /Classes/FilterByTrainer?trainerId=3
        // Called via AJAX when the trainer dropdown changes.
        // Returns only the partial view with the classes list (no full page reload).
        [HttpGet]
        public async Task<IActionResult> FilterByTrainer(int? trainerId)
        {
            var classes = trainerId.HasValue
                ? await _gymClassRepository.GetByTrainerIdAsync(trainerId.Value)
                : await _gymClassRepository.GetAllAsync();

            return PartialView("_ClassesPartial", classes);
        }

        // GET: /Classes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var gymClass = await _gymClassRepository.GetByIdWithDetailsAsync(id);
            if (gymClass == null)
            {
                return NotFound();
            }

            var vm = new ClassDetailsViewModel
            {
                GymClass = gymClass,
                EnrolledMembers = gymClass.Enrollments.Select(e => e.Member)
            };

            return View(vm);
        }
    }
}
