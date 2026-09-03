using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Models
{
    // Simple user store used only for login (per assessment: Email + Password).
    // Any seeded/registered user that isn't the Admin account is treated as a Trainer.
    public class User
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }

        // NOTE: stored as plain text here to keep the course project simple.
        // In a real system this must be hashed (e.g. ASP.NET Core Identity).
        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public bool IsAdmin { get; set; }
    }
}
