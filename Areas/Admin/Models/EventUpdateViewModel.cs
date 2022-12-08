using backProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace backProject.Areas.Admin.Models
{
    public class EventUpdateViewModel
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string Venue { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime StartTime { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime EndTime { get; set; }
        public string ImageUrl { get; set; } = String.Empty;
        public IFormFile? Image { get; set; }
        public List<int>? SpeakersIds { get; set; }
        public List<SelectListItem>? Speakers { get; set; }
    }
}
