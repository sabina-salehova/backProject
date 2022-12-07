using Microsoft.AspNetCore.Mvc;

namespace backProject.Areas.Admin.Controllers
{
    public class TeachersControllerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
