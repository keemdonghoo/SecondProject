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
        private readonly MovieDbContext _movieDbContext;
        private readonly IWriteRepository _writeRepository;

        public HomeController(ILogger<HomeController> logger, MovieDbContext movieDbContext, IWriteRepository writeRepository)
        {
            _logger = logger;
            _movieDbContext = movieDbContext;
            _writeRepository = writeRepository;
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
            var movie = await _movieDbContext.Movies.FirstOrDefaultAsync(m => m.Title == title);
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

        public class ReviewAddModel
        {
            public long MovieUid { get; set; }
            public int Rating { get; set; }
            public string Review { get; set; }
        }

        [HttpPost]
        [Route("Home/ReviewAdd")]
        public async Task<IActionResult> ReviewAdd([FromBody] ReviewAddModel model)
        {
            string userId = HttpContext.Session.GetString("UserId");
            try
            {
                var addReview = new Review
                {
                    MovieUid = model.MovieUid,
                    Rate = model.Rating,
                    Content = model.Review,
                    Date = DateTime.Now,
                    UserId = long.Parse(userId),
                };

                // 리포지토리에서 리뷰 저장 구현
                await _writeRepository.SaveReviewAsync(addReview); // 수정된 부분

                // 리뷰를 성공적으로 추가했음을 나타내는 JSON 객체를 반환
                return Json(new { success = true, review = addReview });
            }
            catch (Exception ex)
            {
                // 에러가 발생했음을 나타내는 JSON 객체를 반환
                return Json(new { success = false, message = ex.Message });
            }
        }


    }
}
