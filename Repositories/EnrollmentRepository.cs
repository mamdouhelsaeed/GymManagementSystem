using GymManagementSystem.Data;
using GymManagementSystem.Models;
using GymManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            return await _context.Enrollments
                .Include(e => e.Member)
                .Include(e => e.GymClass)
                    .ThenInclude(c => c.Trainer)
                .OrderByDescending(e => e.EnrollmentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetByGymClassIdAsync(int gymClassId)
        {
            return await _context.Enrollments
                .Include(e => e.Member)
                .Where(e => e.GymClassId == gymClassId)
                .ToListAsync();
        }

        public async Task<Enrollment?> GetByIdAsync(int id)
        {
            return await _context.Enrollments
                .Include(e => e.Member)
                .Include(e => e.GymClass)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> IsAlreadyEnrolledAsync(int memberId, int gymClassId)
        {
            return await _context.Enrollments
                .AnyAsync(e => e.MemberId == memberId && e.GymClassId == gymClassId);
        }

        public async Task AddAsync(Enrollment enrollment)
        {
            await _context.Enrollments.AddAsync(enrollment);
        }

        public void Update(Enrollment enrollment)
        {
            _context.Enrollments.Update(enrollment);
        }

        public async Task DeleteAsync(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
