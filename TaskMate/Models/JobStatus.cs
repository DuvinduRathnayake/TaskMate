using System.ComponentModel.DataAnnotations;

namespace TaskMate.Models
{
    public class JobStatus
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Status { get; set; } = string.Empty;  
    }
}
