using Microsoft.AspNetCore.Mvc;
using TeamProject.Repositories;

namespace TeamProject.Controllers
{
	public class LikeController : Controller
	{
		private readonly IWriteRepository writeRepository;

		public LikeController(IWriteRepository writeRepository)
		{
			this.writeRepository = writeRepository;
		}

		[HttpPost]
		public async Task<IActionResult> ToggleLike(long postId)
		{
            string userId = HttpContext.Session.GetString("UserId");
            try
			{
				var userUid = long.Parse(userId); // 사용자 UID

				var isLiked = await writeRepository.ToggleLikeAsync(userUid, postId);

				if (isLiked)
				{
					TempData["Notification"] = "글을 좋아요 하셨습니다.";
				}
				else
				{
					TempData["Notification"] = "좋아요를 취소하셨습니다.";
				}

				return RedirectToAction("PostDetail", "Posts", new { id = postId });
			}
			catch (Exception ex)
			{
				// 예외 처리 로직
				// 예외 발생 시 적절한 처리 방법을 선택하십시오.
				// 예를 들어, 로그를 기록하고 오류 페이지로 리디렉션할 수 있습니다.
				return RedirectToAction("Error", "Home");
			}
		}
	}
}
