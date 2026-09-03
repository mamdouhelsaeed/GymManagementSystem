using GymManagementSystem.Filters;
using GymManagementSystem.Models;
using GymManagementSystem.Repositories.Interfaces;
using GymManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementSystem.Controllers
{
    [AdminOnly]
    public class EnrollmentsController : Controller
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IGymClassRepository _gymClassRepository;

        public EnrollmentsController(
            IEnrollmentRepository enrollmentRepository,
            IMemberRepository memberRepository,
            IGymClassRepository gymClassRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _memberRepository = memberRepository;
            _gymClassRepository = gymClassRepository;
        }

        private async Task PopulateDropdownsAsync(EnrollMemberViewModel vm)
        {
            var members = await _memberRepository.GetAllAsync();
            var classes = await _gymClassRepository.GetAllAsync();

            vm.Members = members.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = $"{m.Name} ({m.Email})"
            });

            vm.GymClasses = classes.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"{c.Name} - {c.Trainer.Name}"
            });
        }

        // GET: /Enrollments  (list of all enrollments)
        public async Task<IActionResult> Index()
        {
            var enrollments = await _enrollmentRepository.GetAllAsync();
            return View(enrollments);
        }

        // GET: /Enrollments/Create
        public async Task<IActionResult> Create()
        {
            var vm = new EnrollMemberViewModel();
            await PopulateDropdownsAsync(vm);
            return View(vm);
        }

        // POST: /Enrollments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EnrollMemberViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(vm);
                return View(vm);
            }

            // Prevent duplicate enrollment (nice-to-have requirement)
            var alreadyEnrolled = await _enrollmentRepository.IsAlreadyEnrolledAsync(vm.MemberId, vm.GymClassId);
            if (alreadyEnrolled)
            {
                ModelState.AddModelError(string.Empty, "This member is already enrolled in the selected class.");
                await PopulateDropdownsAsync(vm);
                return View(vm);
            }

            var enrollment = new Enrollment
            {
                MemberId = vm.MemberId,
                GymClassId = vm.GymClassId,
                EnrollmentDate = vm.EnrollmentDate
            };

            await _enrollmentRepository.AddAsync(enrollment);
            await _enrollmentRepository.SaveChangesAsync();

            TempData["Success"] = "Member enrolled successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Enrollments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(id);
            if (enrollment == null) return NotFound();
            return View(enrollment);
        }

        // POST: /Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _enrollmentRepository.DeleteAsync(id);
            await _enrollmentRepository.SaveChangesAsync();
            TempData["Success"] = "Enrollment removed successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
