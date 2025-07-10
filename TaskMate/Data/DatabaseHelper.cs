using System.Data.SqlClient;
using TaskMate.Models;

namespace TaskMate.Data
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Job> GetJob()
        {
            List<Job> jobs = new List<Job>();
            string query = "SELECT Id, Title, Description, StartTime, EndTime, StatusId, PriorityId, UserId FROM Jobs";

            using (var connection = new SqlConnection(_connectionString))
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
    }
}
