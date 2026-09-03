using System.ComponentModel.DataAnnotations;
using GymManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementSystem.ViewModels
{
    public class EnrollMemberViewModel
    {
        [Required(ErrorMessage = "Please select a member.")]
        [Display(Name = "Member")]
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Please select a gym class.")]
        [Display(Name = "Gym Class")]
        public int GymClassId { get; set; }

        [Required(ErrorMessage = "Please select an enrollment date.")]
        [DataType(DataType.Date)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; } = DateTime.Today;

        public IEnumerable<SelectListItem> Members { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> GymClasses { get; set; } = new List<SelectListItem>();

        public IEnumerable<Enrollment> ExistingEnrollments { get; set; } = new List<Enrollment>();
    }
}
