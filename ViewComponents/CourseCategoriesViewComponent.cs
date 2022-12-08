using backProject.DAL.Entities;
using backProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backProject.Models;

namespace backProject.ViewComponents
{
    public class CourseCategoriesViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;
        public CourseCategoriesViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Course> courses = await _dbContext.Courses
                .Where(s => !s.IsDeleted)
                .Include(x => x.Category)
                .ToListAsync();

            List<Category> categories = await _dbContext.Categories
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            var groupByCategoryCourses = courses.GroupBy(x => x.CategoryId);

            return View(new CategoryViewModel
            {
                groupByCategoryCourses=groupByCategoryCourses,
                categories=categories
            });
        }
    }
}
