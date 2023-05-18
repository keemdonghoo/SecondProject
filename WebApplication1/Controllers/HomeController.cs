using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TeamProject.Models;
using Microsoft.Extensions.Logging;

namespace TeamProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Detail(string title)
        {
            // 이곳에서 영화 제목으로 검색하는 로직을 추가하고, 결과를 Detail 뷰에 전달해야 합니다.
            // 예를 들어, 아래 코드처럼 title을 ViewBag에 저장해서 Detail 뷰로 전달할 수 있습니다.
            ViewBag.Title = title;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
