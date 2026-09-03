using GymManagementSystem.Data;
using GymManagementSystem.Models;
using GymManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Repositories
{
    public class GymClassRepository : IGymClassRepository
    {
        private readonly ApplicationDbContext _context;

        public GymClassRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GymClass>> GetAllAsync()
        {
            return await _context.GymClasses
                .Include(c => c.Trainer)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<GymClass>> GetByTrainerIdAsync(int trainerId)
        {
            return await _context.GymClasses
                .Include(c => c.Trainer)
                .Where(c => c.TrainerId == trainerId)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<GymClass?> GetByIdAsync(int id)
        {
            return await _context.GymClasses.FindAsync(id);
        }

        public async Task<GymClass?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.GymClasses
                .Include(c => c.Trainer)
                .Include(c => c.Enrollments)
                    .ThenInclude(e => e.Member)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(GymClass gymClass)
        {
            await _context.GymClasses.AddAsync(gymClass);
        }

        public void Update(GymClass gymClass)
        {
            _context.GymClasses.Update(gymClass);
        }

        public async Task DeleteAsync(int id)
        {
            var gymClass = await _context.GymClasses.FindAsync(id);
            if (gymClass != null)
            {
                _context.GymClasses.Remove(gymClass);
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.GymClasses.AnyAsync(c => c.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
