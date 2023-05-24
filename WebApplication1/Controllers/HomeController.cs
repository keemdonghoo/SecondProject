using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TeamProject.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TeamProject.Data;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace TeamProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MovieDbContext movieDbContext;

        public HomeController(ILogger<HomeController> logger, MovieDbContext movieDbContext)
        {
            _logger = logger;
            this.movieDbContext = movieDbContext;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Detail(string title)
        {
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
