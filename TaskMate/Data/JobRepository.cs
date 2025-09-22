using System.Data.SqlClient;
using TaskMate.Models;

namespace TaskMate.Data
{
    public class JobRepository : IJobRepository
    {
        private readonly DatabaseHelper _db;
        public JobRepository(DatabaseHelper db) => _db = db;

        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            var items = new List<Job>();

            // If your DatabaseHelper has CreateConnection():
            using var conn = _db.CreateConnection();

            // If it doesn't, use this instead (and comment the line above):
            // using var conn = new SqlConnection(_db.ConnectionString);

            using var cmd = new SqlCommand(@"
                SELECT Id, Title, Description, StartTime, EndTime, PriorityId, StatusId, UserId
                FROM Jobs
                ORDER BY Id DESC;", conn);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                items.Add(new Job
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                    StartTime = reader.GetDateTime(3),
                    EndTime = reader.GetDateTime(4),
                    PriorityId = reader.GetInt32(5),
                    StatusId = reader.GetInt32(6),
                    UserId = reader.GetInt32(7)
                });
            }

            return items;
        }

        // We'll implement these in the next step:
        public Task<IEnumerable<JobStatus>> GetStatusesAsync() =>
            Task.FromResult(Enumerable.Empty<JobStatus>());

        public Task<IEnumerable<JobPriority>> GetPrioritiesAsync() =>
            Task.FromResult(Enumerable.Empty<JobPriority>());

        public Task<IEnumerable<User>> GetUsersAsync() =>
            Task.FromResult(Enumerable.Empty<User>());

        public async Task<int> CreateAsync(Job job)
        {
            // Safe defaults so a Title-only form can save
            if (job.StartTime == default) job.StartTime = DateTime.UtcNow;
            if (job.EndTime == default || job.EndTime < job.StartTime)
                job.EndTime = job.StartTime.AddHours(1);
            if (job.StatusId == 0) job.StatusId = 1; // assumes JobStatus Id=1 exists
            if (job.PriorityId == 0) job.PriorityId = 2; // assumes JobPriority Id=2 exists
            if (job.UserId == 0) job.UserId = 1; // assumes Users Id=1 exists

            const string sql = @"
        INSERT INTO Jobs (Title, Description, StartTime, EndTime, StatusId, PriorityId, UserId)
        OUTPUT INSERTED.Id
        VALUES (@Title, @Description, @StartTime, @EndTime, @StatusId, @PriorityId, @UserId);";

            using var conn = new SqlConnection(_db.ConnectionString);
            await conn.OpenAsync();

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Title", job.Title);
            cmd.Parameters.AddWithValue("@Description", (object?)job.Description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@StartTime", job.StartTime);
            cmd.Parameters.AddWithValue("@EndTime", job.EndTime);
            cmd.Parameters.AddWithValue("@StatusId", job.StatusId);
            cmd.Parameters.AddWithValue("@PriorityId", job.PriorityId);
            cmd.Parameters.AddWithValue("@UserId", job.UserId);

            var idObj = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(idObj);
        }


        public async Task<Job?> GetByIdAsync(int id)
    {
        const string sql = @"
        SELECT Id, Title, Description, StartTime, EndTime, PriorityId, StatusId, UserId
        FROM Jobs
        WHERE Id = @Id;";

        using var conn = new SqlConnection(_db.ConnectionString);
        await conn.OpenAsync();

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);

        using var reader = await cmd.ExecuteReaderAsync();
        if (!await reader.ReadAsync()) return null;

        return new Job
        {
            Id = reader.GetInt32(0),
            Title = reader.GetString(1),
            Description = reader.IsDBNull(2) ? null : reader.GetString(2),
            StartTime = reader.GetDateTime(3),
            EndTime = reader.GetDateTime(4),
            PriorityId = reader.GetInt32(5),
            StatusId = reader.GetInt32(6),
            UserId = reader.GetInt32(7)
        };
    }

    public async Task<bool> UpdateAsync(int id, Job job)
    {
        if (job.EndTime < job.StartTime)
            job.EndTime = job.StartTime; // minimal guard

        const string sql = @"
        UPDATE Jobs
        SET Title=@Title,
            Description=@Description,
            StartTime=@StartTime,
            EndTime=@EndTime,
            StatusId=@StatusId,
            PriorityId=@PriorityId,
            UserId=@UserId
        WHERE Id=@Id;";

        using var conn = new SqlConnection(_db.ConnectionString);
        await conn.OpenAsync();

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Parameters.AddWithValue("@Title", job.Title);
        cmd.Parameters.AddWithValue("@Description", (object?)job.Description ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@StartTime", job.StartTime);
        cmd.Parameters.AddWithValue("@EndTime", job.EndTime);
        cmd.Parameters.AddWithValue("@StatusId", job.StatusId);
        cmd.Parameters.AddWithValue("@PriorityId", job.PriorityId);
        cmd.Parameters.AddWithValue("@UserId", job.UserId);

        var rows = await cmd.ExecuteNonQueryAsync();
        return rows > 0;
    }

        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = new SqlConnection(_db.ConnectionString);
            await conn.OpenAsync();

            using var tx = conn.BeginTransaction();

            // 1) Delete dependent rows
            using (var cmdAssign = new SqlCommand(
                "DELETE FROM JobAssignments WHERE JobId = @Id;", conn, tx))
            {
                cmdAssign.Parameters.AddWithValue("@Id", id);
                await cmdAssign.ExecuteNonQueryAsync();
            }

            // 2) Delete the Job
            int rows;
            using (var cmdJob = new SqlCommand(
                "DELETE FROM Jobs WHERE Id = @Id;", conn, tx))
            {
                cmdJob.Parameters.AddWithValue("@Id", id);
                rows = await cmdJob.ExecuteNonQueryAsync();
            }

            tx.Commit();
            return rows > 0;
        }



    }
}
