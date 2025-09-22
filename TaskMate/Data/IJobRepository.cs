using TaskMate.Models;

namespace TaskMate.Data
{
   public interface IJobRepository {

        Task<IEnumerable<Job>> GetAllAsync();     // list page
        Task<IEnumerable<JobStatus>> GetStatusesAsync();   // dropdowns
        Task<IEnumerable<JobPriority>> GetPrioritiesAsync();
        Task<IEnumerable<User>> GetUsersAsync();
        Task<int> CreateAsync(Job job);
    }
}
