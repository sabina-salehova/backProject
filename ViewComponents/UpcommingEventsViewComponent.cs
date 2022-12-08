using backProject.DAL.Entities;
using backProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backProject.ViewComponents
{
    public class UpcommingEventsViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;
        public UpcommingEventsViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Event> events = await _dbContext.Events
                .Where(s => !s.IsDeleted & DateTime.Compare(s.StartTime, DateTime.Now) == 1)
                .ToListAsync();

            return View(events);
        }
    }
}
