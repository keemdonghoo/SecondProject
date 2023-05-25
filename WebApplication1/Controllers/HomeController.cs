using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TeamProject.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TeamProject.Data;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using TeamProject.Models.Domain;
using TeamProject.Models.ViewModels;
using TeamProject.Repositories;

namespace TeamProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MovieDbContext movieDbContext;
        private readonly WriteRepository writeRepository;

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

        [HttpGet]
        [Route("Home/Detail")]
        public async Task<IActionResult> Detail(string title)
        {
            var movie = await movieDbContext.Movies.FirstOrDefaultAsync(m => m.Title == title);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> ReviewAdd(long movieId, int rating, string review)
        {
            string userId = HttpContext.Session.GetString("UserId");
            try
            {
                var addReview = new Review
                {
                    MovieId = movieId,
                    Rate = rating,
                    Content = review,
                    Date = DateTime.Now,
                    UserId = long.Parse(userId),
                };

                //리포지토리에서 리뷰 저장 구현
                await writeRepository.SaveReviewAsync(addReview);

                return RedirectToAction("Detail", "Home", new { id = movieId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
