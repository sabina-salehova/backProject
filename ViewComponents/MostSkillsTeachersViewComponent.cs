using backProject.DAL;
using backProject.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backProject.ViewComponents
{
    public class MostSkillsTeachersViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;
        public MostSkillsTeachersViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Teacher> teachers = await _dbContext.Teachers
                .Where(s => !s.IsDeleted)
                .OrderByDescending(t => (t.LanguageProgress+t.InnovationProgress+t.CommunicationProgress+t.DesignProgress+t.DevelopmentProgress+t.TeamLeaderProgress))
                .Take(4)
                .ToListAsync();                

            return View(teachers);
        }
    }
}
