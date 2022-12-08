using backProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace backProject.Areas.Admin.Models
{
    public class EventCreateViewModel
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string Venue { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public IFormFile Image { get; set; }
        public List<int>? SpeakersIds { get; set; }
        public List<SelectListItem>? Speakers { get; set; }
        public List<Speaker>? AllSpekaers { get; set; }
    }
}
