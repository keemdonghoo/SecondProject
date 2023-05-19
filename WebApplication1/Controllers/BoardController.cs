using Microsoft.AspNetCore.Mvc;
using TeamProject.Repositories;
using TeamProject.Common;
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
        [HttpGet]
        public async Task<IActionResult> List(
            long boardId,
            [FromQuery(Name = "page")] int? page   // int? 타입.  만약 page parameter 가 없거나, 변환 안되는 값이면 null 값
            )
        {
            page ??= 1;   // 디폴트는 1 page
            if (page < 1) page = 1;

            int writePages = HttpContext.Session.GetInt32("writePages") ?? Page.WRITE_PAGES;
            int pageRows = HttpContext.Session.GetInt32("pageRows") ?? Page.PAGE_ROWS;

            HttpContext.Session.SetInt32("page", (int)page);
            
            
            
            int totalPage = (int)Math.Ceiling(cnt / (double)pageRows);

            HttpContext.Session.SetInt32("page", (int)page);
            var posts = await writeRepository.GetAllPostAsync(boardId, pageNum, pageSize);
            return View(posts);
        }
    }
}
