using TaskMate.Models;

namespace TaskMate.Data
{
   public interface IJobRepository {

        Task<IEnumerable<Job>> GetAllAsync();     // list page
        Task<IEnumerable<JobStatus>> GetStatusesAsync();   // dropdowns
        Task<IEnumerable<JobPriority>> GetPrioritiesAsync();
        Task<IEnumerable<User>> GetUsersAsync();
        Task<Job?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, Job job);

        Task<bool> DeleteAsync(int id);

        Task<int> CreateAsync(Job job);
    }
}
