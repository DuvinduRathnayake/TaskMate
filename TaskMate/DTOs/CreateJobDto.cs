using System.ComponentModel.DataAnnotations;

namespace TaskMate.DTOs
{
    public class CreateJobDto
    {
        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [Required] public int StatusId { get; set; }
        [Required] public int PriorityId { get; set; }
        [Required] public int UserId { get; set; }
    }
}
