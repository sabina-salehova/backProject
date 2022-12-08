using backProject.DAL.Entities;
using backProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backProject.ViewComponents
{
    public class LatestCoursesViewComponent: ViewComponent
    {
        private readonly AppDbContext _dbContext;
        public LatestCoursesViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Course> courses = await _dbContext.Courses
                .Where(s => !s.IsDeleted)
                .Include(x => x.Category)
                .OrderByDescending(t => t.Starts)
                .Take(3)
                .ToListAsync();

            return View(courses);
        }
    }
}
