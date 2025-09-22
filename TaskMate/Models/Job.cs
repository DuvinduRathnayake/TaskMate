using System.ComponentModel.DataAnnotations;

namespace TaskMate.Models
{
    public class Job
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(5000)]
        public string? Description { get; set; }

        // Keep these non-nullable if your DB requires them; add UI hints:
        [Display(Name = "Start Time"), DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time"), DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        [Required, Display(Name = "Priority")]
        public int PriorityId { get; set; }

        [Required, Display(Name = "Status")]
        public int StatusId { get; set; }

        // If assignment is optional, make this nullable:
        public int UserId { get; set; }  // or: public int? UserId { get; set; }
    }
}
