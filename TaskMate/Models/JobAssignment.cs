namespace TaskMate.Models
{
    public class JobAssignment
    {
        public int JobId { get; set; }
        public int UserId { get; set; }

        public Job? Job { get; set; }
        public User? User { get; set; }
    }
}
