using Microsoft.AspNetCore.Mvc;
using TaskMate.Data;
using TaskMate.DTOs;

namespace TaskMate.Controllers
{
    [Route("api/job")]
    [ApiController]
    public class JobApiController : ControllerBase
    {
        private readonly DatabaseHelper _databaseHelper;

        //Constructor 
        public JobApiController(DatabaseHelper databaseHelper) => _databaseHelper = databaseHelper;

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

        [HttpPost]
        public async Task<IActionResult> CreateJob([FromBody] CreateJobDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int id = await _databaseHelper.CraeteJobAsync(dto);

            var result = new
            {
                Id = id,
                dto.Title,
                dto.Description,
                dto.StartTime,
                dto.EndTime,
                dto.StatusId,
                dto.PriorityId,
                dto.UserId
            };

            return CreatedAtAction(nameof(GetJob), 
                               new { id },
                               result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetJob(int id)
        {
            var job = await _databaseHelper.GetJobAsync(id);
            return job is null ? NotFound() : Ok(job);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateJob(int id, [FromBody] UpdateJobDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the job exists
            var existingJob = await _databaseHelper.GetJobAsync(id);
            if (existingJob == null)
            {
                return NotFound($"Job with ID {id} not found.");
            }

            // Update the job in the database
            bool isUpdated = await _databaseHelper.UpdateJobAsync(id, dto);

            if (isUpdated)
            {
                return NoContent(); // Successfully updated
            }

            return StatusCode(500, "Failed to update the job.");
        }




    }
}
