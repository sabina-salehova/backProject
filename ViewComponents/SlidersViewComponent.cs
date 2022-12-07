using backProject.DAL.Entities;
using backProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backProject.ViewComponents
{
    public class SlidersViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;
        public SlidersViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Slider> sliders = await _dbContext.Sliders.Where(s => !s.IsDeleted).ToListAsync();

            return View(sliders);
        }
    }
}
