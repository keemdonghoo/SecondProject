using Microsoft.AspNetCore.Mvc;

namespace TeamProject.Controllers
{
    public class MyPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
