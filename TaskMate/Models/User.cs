using System.ComponentModel.DataAnnotations;

namespace TaskMate.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress, StringLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Role { get; set; } = string.Empty; // e.g., Admin/Manager/Member
    }
}
