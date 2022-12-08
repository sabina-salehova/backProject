using Microsoft.AspNetCore.Mvc;

namespace backProject.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
