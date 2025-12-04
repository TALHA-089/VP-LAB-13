using Microsoft.AspNetCore.Mvc;

namespace VPLab13.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
