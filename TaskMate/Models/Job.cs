namespace TaskMate.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int PriorityId { get; set; }
        public int StatusId { get; set; }
        public int UserId {  get; set; }
    }
}
