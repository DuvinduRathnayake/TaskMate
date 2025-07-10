namespace TaskMate.Models
{
    public class JobAssignment
    {
        public int JobId { get; set; }      // Foreign key to Job
        public int UserId { get; set; }     // Foreign key to User

        public Job Job { get; set; }        // Navigation property 
        public User User { get; set; }
    }
}
