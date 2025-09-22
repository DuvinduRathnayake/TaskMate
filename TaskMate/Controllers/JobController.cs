using Microsoft.AspNetCore.Mvc;
using TaskMate.Data;
using TaskMate.Models;

namespace TaskMate.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobRepository _repo;

        //Constructor to inject repository here
        public JobController(IJobRepository repo)
        {
            _repo = repo;
        }

        //Get jobs
        public async Task<IActionResult> Index()
        {
            var jobs = await _repo.GetAllAsync();
            return View(jobs);
        }

        //Create jobs
        public IActionResult Create()
        {
            return View(new Job());
        }

        //Post method to create jobs
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,StartTime,EndTime,PriorityId,StatusId,UserId")] Job job)
        {
            if (job.EndTime < job.StartTime)
                ModelState.AddModelError(nameof(Job.EndTime), "End Time cannot be earlier than Start Time.");

            if (!ModelState.IsValid)
                return View(job);

            await _repo.CreateAsync(job);
            return RedirectToAction(nameof(Index));
        }



    }
}
