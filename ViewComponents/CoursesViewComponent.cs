using backProject.DAL.Entities;
using backProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backProject.ViewComponents
{
    public class CoursesViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;
        public CoursesViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Course> courses = await _dbContext.Courses
                .Where(s => !s.IsDeleted)
                .Include(x => x.Category)
                .ToListAsync();

            return View(courses);
        }
    }
}
