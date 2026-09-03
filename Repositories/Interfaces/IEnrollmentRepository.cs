using GymManagementSystem.Models;

namespace GymManagementSystem.Repositories.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<IEnumerable<Enrollment>> GetAllAsync();
        Task<IEnumerable<Enrollment>> GetByGymClassIdAsync(int gymClassId);
        Task<Enrollment?> GetByIdAsync(int id);
        Task<bool> IsAlreadyEnrolledAsync(int memberId, int gymClassId);
        Task AddAsync(Enrollment enrollment);
        void Update(Enrollment enrollment);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
