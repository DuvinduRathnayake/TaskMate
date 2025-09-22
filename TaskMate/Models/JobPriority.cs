using System.ComponentModel.DataAnnotations;

namespace TaskMate.Models
{
    public class JobPriority
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Priority { get; set; } = string.Empty;  // e.g., Low/Medium/High
    }
}
