using GymManagementSystem.Models;

namespace GymManagementSystem.Repositories.Interfaces
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetAllAsync();
        Task<Member?> GetByIdAsync(int id);
        Task<Member?> GetByIdWithEnrollmentsAsync(int id);
        Task AddAsync(Member member);
        void Update(Member member);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task SaveChangesAsync();
    }
}
