using backProject.Areas.Admin.Models;
using backProject.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace backProject.Areas.Admin.Services
{
    public class CategoryService
    {
        private readonly AppDbContext _dbContext;
        public CategoryService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<CourseCreateViewModel> GetCategories()
        {
            var categories = await _dbContext.Categories.Where(c => !c.IsDeleted).Include(x => x.Courses).ToListAsync();

            var categoriesSelectListItem = new List<SelectListItem>();

            categories.ForEach(x => categoriesSelectListItem.Add(new SelectListItem(x.Name, x.Id.ToString())));

            var model = new CourseCreateViewModel
            {
                Categories = categoriesSelectListItem,
                AllCategories=categories
            };

            return model;
        }
    }
}
