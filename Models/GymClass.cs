using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagementSystem.Models
{
    public class GymClass
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Class name is required.")]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Schedule is required.")]
        [StringLength(100)]
        [Display(Name = "Schedule")]
        public string Schedule { get; set; }

        [Required]
        [Display(Name = "Trainer")]
        public int TrainerId { get; set; }

        [ForeignKey(nameof(TrainerId))]
        public Trainer Trainer { get; set; }

        // Navigation property: many-to-many with Member through Enrollment
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
