using GymManagementSystem.Models;

namespace GymManagementSystem.ViewModels
{
    public class ClassDetailsViewModel
    {
        public GymClass GymClass { get; set; }
        public IEnumerable<Member> EnrolledMembers { get; set; } = new List<Member>();
    }
}
