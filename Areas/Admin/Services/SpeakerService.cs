using backProject.Areas.Admin.Models;
using backProject.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace backProject.Areas.Admin.Services
{
    public class SpeakerService
    {
        private readonly AppDbContext _dbContext;

        public SpeakerService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<EventCreateViewModel> GetSpeakers()
        {
            var speakers = await _dbContext.Speakers.Where(c => !c.IsDeleted).Include(x => x.EventSpeakers).ThenInclude(x => x.Event).ToListAsync();

            var speakersSelectListItem = new List<SelectListItem>();

            speakers.ForEach(x => speakersSelectListItem.Add(new SelectListItem(x.Name + " " + x.Surname, x.Id.ToString())));

            var model = new EventCreateViewModel
            {
                Speakers = speakersSelectListItem,
                AllSpekaers=speakers
            };

            return model;
        }
    }
}
