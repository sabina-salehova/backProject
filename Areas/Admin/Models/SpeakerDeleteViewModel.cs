using backProject.DAL.Entities;

namespace backProject.Areas.Admin.Models
{
    public class SpeakerDeleteViewModel
    {
        public List<Speaker>? Speakers { get; set; }
        public List<EventSpeaker>? EventSpeakers { get; set; }
    }
}
