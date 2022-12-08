using backProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace backProject.Areas.Admin.Models
{
    public class BlogCreateViewModel
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string Writer { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime Date { get; set; }
        public int CommentCount { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
        public List<SelectListItem>? Categories { get; set; }
        public List<Category>? AllCategories { get; set; }
    }
}
