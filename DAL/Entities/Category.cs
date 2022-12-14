using System.Reflection.Metadata;

namespace backProject.DAL.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public List<Course> Courses { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
