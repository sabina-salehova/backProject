using backProject.DAL.Entities;

namespace backProject.Areas.Admin.Models
{
    public class EventDetailsViewModel
    {
        public Event Eventt { get; set; }
        public List<Speaker> Speakers { get; set; }
    }
}
