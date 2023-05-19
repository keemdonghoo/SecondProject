using Microsoft.AspNetCore.Mvc;
using TeamProject.Repositories;

namespace TeamProject.Controllers
{
    public class CommentController : Controller
    {
        private readonly IWriteRepository writeRepository;

        public CommentController(IWriteRepository writeRepository)
        {
            this.writeRepository = writeRepository;
        }

        [HttpGet("comment/userscommentlist/{userId}")]
        public async Task<IActionResult> UsersCommentList(long userId)
        {
            var comments = await writeRepository.GetUserCommentAsync(userId);
            return View(comments);
        }
    }
}
