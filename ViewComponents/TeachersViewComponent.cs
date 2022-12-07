using backProject.DAL.Entities;
using backProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backProject.ViewComponents
{
    public class TeachersViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;
        public TeachersViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Teacher> teachers = await _dbContext.Teachers.Where(s => !s.IsDeleted).ToListAsync();

            return View(teachers);
        }
    }
}
