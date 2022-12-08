using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace backProject.Areas.Admin.Models
{
    public class BlogUpdateViewModel
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string Writer { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime Date { get; set; }
        public int CommentCount { get; set; }
        public IFormFile? Image { get; set; }
        public string ImageUrl { get; set; } = String.Empty;
        public int CategoryId { get; set; }
        public List<SelectListItem>? Categories { get; set; }
    }
}
