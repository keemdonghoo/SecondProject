using Microsoft.AspNetCore.Mvc;
using TeamProject.Repositories;

namespace TeamProject.Controllers
{
    //게시판 컨트롤러
    public class BoardController : Controller
    {
        private readonly IWriteRepository writeRepository;

        public BoardController(IWriteRepository writeRepository)
        {
            this.writeRepository = writeRepository;
        }

		//게시글 목록 읽어오기
		[HttpGet("board/list/{boardId}")]
		public async Task<IActionResult> List(long boardId)
        {
            var posts = await writeRepository.GetAllPostAsync(boardId);
            return View(posts);
        }
    }
}
