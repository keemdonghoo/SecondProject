using Microsoft.AspNetCore.Mvc;
using TeamProject.Models.Domain;
using TeamProject.Models.ViewModels;
using TeamProject.Repositories;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace TeamProject.Controllers
{
	public class CommentController : Controller
	{
		private readonly IWriteRepository writeRepository;

		public CommentController(IWriteRepository writeRepository)
		{
			this.writeRepository = writeRepository;
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{

			AddCommentRequest addCommentRequest = new()
			{
				// TempData 에 담아둔 내용 꺼내가기  (꺼내가면 자동 소멸됨)
				Content = (string)TempData["Content"],

			};
			return View(addCommentRequest);

		}

		

		[HttpPost]
		public async Task<IActionResult> Add(AddCommentRequest addCommentRequest,int postId)
		{
			addCommentRequest.Validate();
			if (addCommentRequest.HasError)
			{
				TempData["ContentError"] = addCommentRequest.ErrorContent;


				TempData["Content"] = addCommentRequest.Content;

				return RedirectToAction("PostDetail", "Posts", new { id = postId });
			};


			try
			{

				// 댓글 작성 로직
				var comment = new Comment
				{
					PostId = postId,
					Content = addCommentRequest.Content,
					RegDate = DateTime.Now,
					UserId = 2,
				};

				await writeRepository.AddCommentAsync(comment);

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

		
        [HttpPost]
        public async Task<IActionResult> DeleteComment(long CommentId)
        {
            var deleteComment = await writeRepository.DeleteCommentAsync(CommentId);

            if (deleteComment != null)
            {
                // 삭제 성공
                return View("Delete", 1);
            }

            // 삭제 실패
            return View("Delete", 0);

        }


	



		[HttpGet("comment/userscommentlist/{userId}")]
		public async Task<IActionResult> UsersCommentList(long userId)
		{
			var comments = await writeRepository.GetUserCommentAsync(userId);
			return View(comments);
		}
	}
}
