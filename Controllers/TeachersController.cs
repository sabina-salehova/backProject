using backProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backProject.Controllers
{
    public class TeachersController : Controller
    {
        private readonly AppDbContext _dbContext;

        public TeachersController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();

            var teacher = await _dbContext.Teachers.SingleOrDefaultAsync(t => t.Id == id);

            if (teacher is null) return NotFound();

            return View(teacher);
        }
    }
}
