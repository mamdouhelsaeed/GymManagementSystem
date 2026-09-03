using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Models
{
    public class Trainer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Trainer name is required.")]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Specialization is required.")]
        [StringLength(100)]
        [Display(Name = "Specialization")]
        public string Specialization { get; set; }

        // Navigation property: one trainer -> many classes
        public ICollection<GymClass> GymClasses { get; set; } = new List<GymClass>();
    }
}
