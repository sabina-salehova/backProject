using backProject.DAL.Entities;
using backProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backProject.ViewComponents
{
    public class EventsViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;
        public EventsViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Event> events = await _dbContext.Events
                .Where(s => !s.IsDeleted)
                .Include(x => x.EventSpeakers)
                .ToListAsync();

            return View(events);
        }
    }
}
