using GymManagementSystem.Filters;
using GymManagementSystem.Models;
using GymManagementSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers
{
    [AdminOnly]
    public class MembersController : Controller
    {
        private readonly IMemberRepository _memberRepository;

        public MembersController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        // GET: /Members
        public async Task<IActionResult> Index()
        {
            var members = await _memberRepository.GetAllAsync();
            return View(members);
        }

        // GET: /Members/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var member = await _memberRepository.GetByIdWithEnrollmentsAsync(id);
            if (member == null) return NotFound();
            return View(member);
        }

        // GET: /Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Members/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,Phone")] Member member)
        {
            if (!ModelState.IsValid) return View(member);

            await _memberRepository.AddAsync(member);
            await _memberRepository.SaveChangesAsync();
            TempData["Success"] = "Member created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Members/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null) return NotFound();
            return View(member);
        }

        // POST: /Members/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Phone")] Member member)
        {
            if (id != member.Id) return NotFound();
            if (!ModelState.IsValid) return View(member);

            _memberRepository.Update(member);
            await _memberRepository.SaveChangesAsync();
            TempData["Success"] = "Member updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Members/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null) return NotFound();
            return View(member);
        }

        // POST: /Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _memberRepository.DeleteAsync(id);
            await _memberRepository.SaveChangesAsync();
            TempData["Success"] = "Member deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
