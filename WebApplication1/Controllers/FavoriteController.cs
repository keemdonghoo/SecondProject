using Microsoft.AspNetCore.Mvc;
using TeamProject.Repositories;

namespace TeamProject.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IWriteRepository writeRepository;

        public FavoriteController(IWriteRepository writeRepository)
        {
            this.writeRepository = writeRepository;
        }

        [HttpPost]
        public async Task<IActionResult> ToggleFavorite()
        {
            long userUid = long.Parse(HttpContext.Session.GetString("UserId"));
            long movieId = long.Parse(HttpContext.Session.GetString("MovieId"));
            string movieTitle = HttpContext.Session.GetString("Title");

            try
            {        
                var isFavorited = await writeRepository.ToggleFavoriteAsync(userUid, movieId);

				if (isFavorited)
				{
					 TempData["Notification"] = "재생목록에 추가되었습니다.";
				}
				else
				{
					TempData["Notification"] = "재생목록에서 제거되었습니다.";
				}

				return RedirectToAction("Detail", "Home", new { title = movieTitle });

            }
            catch (Exception ex)
            {
                // 예외 처리 로직
                // 예외 발생 시 적절한 처리 방법을 선택하십시오.
                // 예를 들어, 로그를 기록하고 오류 페이지로 리디렉션할 수 있습니다.
                return RedirectToAction("Error", "Home");
            }
        }


        [HttpGet("favorite/usersfavoritelist/{userId}")]
        public async Task<IActionResult> UsersFavoriteList()
        {
            long userId = long.Parse(HttpContext.Session.GetString("UserId"));

            var favorites = await writeRepository.GetUserFavoriteAsync(userId);
            return View(favorites);
        }
    }
}
