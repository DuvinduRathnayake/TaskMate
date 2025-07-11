using Microsoft.AspNetCore.Mvc;
using TaskMate.Data;

namespace TaskMate.Controllers
{
    public class JobController : Controller
    {
        private readonly DatabaseHelper _databaseHelper;

        public JobController(DatabaseHelper databaseHelper) { }
    }
}
