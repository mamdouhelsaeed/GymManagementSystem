using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagementSystem.Models
{
    // Join entity that implements the Member <-> GymClass many-to-many relationship
    public class Enrollment
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Member")]
        public int MemberId { get; set; }

        [ForeignKey(nameof(MemberId))]
        public Member Member { get; set; }

        [Required]
        [Display(Name = "Gym Class")]
        public int GymClassId { get; set; }

        [ForeignKey(nameof(GymClassId))]
        public GymClass GymClass { get; set; }

        [Required]
        [Display(Name = "Enrollment Date")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; } = DateTime.Today;
    }
}
