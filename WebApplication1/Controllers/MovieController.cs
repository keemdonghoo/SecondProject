using Microsoft.AspNetCore.Mvc;

namespace TeamProject.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
