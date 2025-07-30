namespace TaskMate.DTOs
{
    public class UpdateJobDto
    {
        public string Title { get; set; }        // Task Title
        public string Description { get; set; }  // Task Description
        public DateTime StartTime { get; set; }   // Start Time
        public DateTime EndTime { get; set; }     // End Time
        public int StatusId { get; set; }         // Status (e.g., In Progress, Completed)
        public int PriorityId { get; set; }       // Priority (e.g., Low, Medium, High)
        public int UserId { get; set; }           // Assigned User
    }
}
