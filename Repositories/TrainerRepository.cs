using GymManagementSystem.Data;
using GymManagementSystem.Models;
using GymManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Repositories
{
    public class TrainerRepository : ITrainerRepository
    {
        private readonly ApplicationDbContext _context;

        public TrainerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trainer>> GetAllAsync()
        {
            return await _context.Trainers
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<Trainer?> GetByIdAsync(int id)
        {
            return await _context.Trainers.FindAsync(id);
        }

        public async Task<Trainer?> GetByIdWithClassesAsync(int id)
        {
            return await _context.Trainers
                .Include(t => t.GymClasses)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(Trainer trainer)
        {
            await _context.Trainers.AddAsync(trainer);
        }

        public void Update(Trainer trainer)
        {
            _context.Trainers.Update(trainer);
        }

        public async Task DeleteAsync(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer != null)
            {
                _context.Trainers.Remove(trainer);
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Trainers.AnyAsync(t => t.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
