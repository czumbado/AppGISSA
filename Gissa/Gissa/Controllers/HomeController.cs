using Microsoft.AspNetCore.Mvc;

namespace Gissa.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
