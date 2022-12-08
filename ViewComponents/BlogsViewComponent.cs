using backProject.DAL.Entities;
using backProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backProject.ViewComponents
{
    public class BlogsViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;
        public BlogsViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Blog> blogs = await _dbContext.Blogs
                .Where(s => !s.IsDeleted)
                .Include(x => x.Category)
                .ToListAsync();

            return View(blogs);
        }
    }
}
