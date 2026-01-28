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

        public async Task<IActionResult> Edit(int id)
        {
            var job = await _repo.GetByIdAsync(id);
            return job is null ? NotFound() : View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Job job)
        {
            if (job.EndTime < job.StartTime)
                ModelState.AddModelError(nameof(Job.EndTime), "End Time cannot be earlier than Start Time.");

            if (!ModelState.IsValid) return View(job);

            var ok = await _repo.UpdateAsync(id, job);
            return ok ? RedirectToAction(nameof(Index)) : Problem("Update failed.");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var job = await _repo.GetByIdAsync(id);
            return job is null ? NotFound() : View(job);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _repo.DeleteAsync(id);
            return ok ? RedirectToAction(nameof(Index)) : Problem("Delete failed.");
        }



    }
}
