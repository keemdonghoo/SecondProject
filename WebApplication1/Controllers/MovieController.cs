using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
