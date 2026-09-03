using GymManagementSystem.Models;

namespace GymManagementSystem.Repositories.Interfaces
{
    public interface ITrainerRepository
    {
        Task<IEnumerable<Trainer>> GetAllAsync();
        Task<Trainer?> GetByIdAsync(int id);
        Task<Trainer?> GetByIdWithClassesAsync(int id);
        Task AddAsync(Trainer trainer);
        void Update(Trainer trainer);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task SaveChangesAsync();
    }
}
