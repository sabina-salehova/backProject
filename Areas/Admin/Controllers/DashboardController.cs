using Microsoft.AspNetCore.Mvc;

namespace backProject.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
