using backProject.DAL.Entities;

namespace backProject.Areas.Admin.Models
{
    public class CategoryDeleteViewModel
    {
        public List<Course>? AllCourses { get; set; }
        public List<Category>? Allcategories { get; set; }
        public List<Blog>? AllBlogs { get; set; }
    }
}
