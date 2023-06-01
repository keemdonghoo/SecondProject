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
using System.Text.Json;

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

            HttpContext.Session.SetString("MovieUid", movie.MovieUid.ToString());
            HttpContext.Session.SetString("Title", movie.Title);
            HttpContext.Session.SetString("MovieId",movie.Id.ToString());

            if (movie == null)
            {
                return NotFound();
            }
            var reviews = await _writeRepository.GetIdReviewAsync(movie.MovieUid);
            movie.Reviews = reviews;

            int commentCount = reviews?.Count ?? 0;

            ViewData["CommentCount"] = commentCount;

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
        public async Task<IActionResult> ReviewAdd([FromBody] ReviewAddModel model)
        {
            string userId = HttpContext.Session.GetString("UserId");
            string movieUid = HttpContext.Session.GetString("MovieUid");
            string movieTitle = HttpContext.Session.GetString("Title");
			string movieId = HttpContext.Session.GetString("MovieId");

			if (userId == null)
            {
                return null;
            }
          
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
                await _writeRepository.SaveReviewAsync(addReview);


                // 성공적으로 리뷰를 추가한 후 'Detail' 액션으로 리다이렉트합니다.
                return RedirectToAction("Detail", "Home", new { title = movieTitle });
            }
            catch (Exception ex)
            {
                // 에러가 발생했음을 나타내는 JSON 객체를 반환
                return Json(new { success = false, message = ex.Message });
            }
        }


		[HttpPost]
		public async Task<IActionResult> DeleteReview(long ReviewId)
		{
			var deleteReview = await _writeRepository.DeleteReviewAsync(ReviewId);

			if (deleteReview != null)
			{
				// 삭제 성공
				return View("Delete", 1);
			}

			// 삭제 실패
			return View("Delete", 0);

		}



	}
}
