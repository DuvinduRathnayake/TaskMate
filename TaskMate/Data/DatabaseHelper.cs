using System.Data.SqlClient;
using TaskMate.Models;
using Dapper;
using TaskMate.DTOs;

namespace TaskMate.Data
{
    public class DatabaseHelper
    {
        private readonly string _connectionStrings;

        public DatabaseHelper(string connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }

        public bool TestConnection()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionStrings))
                {
                    connection.Open();  
                    return true;  
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");  
                return false;  
            }
        }

        public List<Job> GetJobs()
        {
            List<Job> jobs = new List<Job>();
            string query = "SELECT Id, Title, Description, StartTime, EndTime, StatusId, PriorityId, UserId FROM Jobs";

            using (var connection = new SqlConnection(_connectionStrings))
            {
                SqlCommand sqlCommand = new SqlCommand(query, connection); 
                connection.Open();

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var job = new Job
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            StartTime = reader.GetDateTime(reader.GetOrdinal("StartTime")),
                            EndTime = reader.GetDateTime(reader.GetOrdinal("EndTime")),
                            StatusId = reader.GetInt32(reader.GetOrdinal("StatusId")),
                            PriorityId = reader.GetInt32(reader.GetOrdinal("PriorityId")),
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId"))
                        };
                        jobs.Add(job);
                    }
                }
            }

            return jobs;
        }

        public async Task<int> CraeteJobAsync(CreateJobDto dto)
        {
            const string sql = @"
                               INSERT INTO Jobs (Title, Description, StartTime, EndTime, StatusId, PriorityId, UserId)
                               OUTPUT INSERTED.Id
                               VALUES (@Title, @Description, @StartTime, @EndTime, @StatusId, @PriorityId, @UserId);";

            await using var conn = new SqlConnection(_connectionStrings);
            await conn.OpenAsync();
            return await conn.ExecuteScalarAsync<int>(sql,dto); ;
        }

        public async Task<Job?> GetJobAsync(int id)
        {
            const string sql = "SELECT * FROM Jobs WHERE Id = @Id";

            await using var conn = new SqlConnection(_connectionStrings);
            await conn.OpenAsync();

            return await conn.QueryFirstOrDefaultAsync<Job>(sql, new { Id = id });
        }

    }
}
