using backProject.DAL.Entities;
using backProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backProject.ViewComponents
{
    public class TestimonialSlidersViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;
        public TestimonialSlidersViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<TestimonialSlider> testimonialSliders = await _dbContext.TestimonialSliders.Where(s => !s.IsDeleted).ToListAsync();

            return View(testimonialSliders);
        }
    }
}
