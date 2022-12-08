using backProject.DAL.Entities;

namespace backProject.Models
{
    public class CategoryViewModel
    {
        public List<Category> categories= new List<Category>();
        public IEnumerable<IGrouping<int, Course>>? groupByCategoryCourses;
    }
}
