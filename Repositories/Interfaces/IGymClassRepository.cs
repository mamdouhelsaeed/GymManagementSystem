using GymManagementSystem.Models;

namespace GymManagementSystem.Repositories.Interfaces
{
    public interface IGymClassRepository
    {
        Task<IEnumerable<GymClass>> GetAllAsync();
        Task<IEnumerable<GymClass>> GetByTrainerIdAsync(int trainerId);
        Task<GymClass?> GetByIdAsync(int id);
        Task<GymClass?> GetByIdWithDetailsAsync(int id);
        Task AddAsync(GymClass gymClass);
        void Update(GymClass gymClass);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task SaveChangesAsync();
    }
}
