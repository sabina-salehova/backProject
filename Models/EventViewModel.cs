using backProject.DAL.Entities;

namespace backProject.Models
{
    public class EventViewModel
    {
        public Event eventt { get; set; } = new Event();
        public List<Speaker> speakers { get; set; } = new List<Speaker>();
    }
}
