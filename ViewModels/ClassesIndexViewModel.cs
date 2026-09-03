using GymManagementSystem.Models;

namespace GymManagementSystem.ViewModels
{
    public class ClassesIndexViewModel
    {
        public int? SelectedTrainerId { get; set; }
        public IEnumerable<Trainer> Trainers { get; set; } = new List<Trainer>();
        public IEnumerable<GymClass> Classes { get; set; } = new List<GymClass>();
    }
}
