using Microsoft.AspNetCore.Mvc;
using TaskMate.Data;

namespace TaskMate.Controllers
{
    [Route("api/job")]
    [ApiController]
    public class JobApiController : ControllerBase
    {
        private readonly DatabaseHelper _databaseHelper;

        public JobApiController(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        [HttpGet]
        public IActionResult GetJobs()
        {
            // Test the connection
            bool isConnected = _databaseHelper.TestConnection();
            if (!isConnected)
            {
                return StatusCode(500, "Database connection failed.");
            }

            var jobs = _databaseHelper.GetJobs();
            if (jobs.Count > 0)
            {
                return Ok(jobs);
            }
            else
            {
                return NotFound("No Jobs Found");
            }
        }
    }
}
