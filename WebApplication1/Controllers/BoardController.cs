using Microsoft.AspNetCore.Mvc;
using TeamProject.Repositories;
using TeamProject.Common;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

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
        public async Task<IActionResult> List(
            long boardId,
            [FromQuery(Name = "page")] int? page   // int? 타입.  만약 page parameter 가 없거나, 변환 안되는 값이면 null 값
            )
        {
            boardId = 1;
            page ??= 1;   // 디폴트는 1 page
            if (page < 1) page = 1;

            int writePages = HttpContext.Session.GetInt32("writePages") ?? Page.WRITE_PAGES;
            int pageRows = HttpContext.Session.GetInt32("pageRows") ?? Page.PAGE_ROWS;

            HttpContext.Session.SetInt32("page", (int)page);

            long cnt = await writeRepository.CountAsync();
            int totalPage = (int)Math.Ceiling(cnt / (double)pageRows);

            if (page > totalPage) page = totalPage;
            int fromRow = ((int)page - 1) * pageRows;
            if(fromRow < 0) fromRow = 1;

            // [페이징] 에 표시할 '시작페이지' 와 '마지막 페이지' 계산
            int startPage = ((((int)page - 1) / writePages) * writePages) + 1;
            int endPage = startPage + writePages - 1;
            if (endPage >= totalPage) endPage = totalPage;

            // 위 값들을 View에 보내주기
            ViewData["cnt"] = cnt;  // 전체글개수
            ViewData["page"] = page;  // 현재 페이지
            ViewData["totalPage"] = totalPage;  // 총 '페이지' 수 
            ViewData["pageRows"] = pageRows;  // 한 '페이지' 에 표시할 글 개수

            // [페이징]            
            ViewData["url"] = $"{Request.Scheme}://{Request.Host}{Request.PathBase}{Request.Path}";  // 목록 url
            ViewData["writePages"] = writePages; // [페이징] 에 표시할 숫자 개수
            ViewData["startPage"] = startPage; // [페이징] 에 표시할 시작 페이지
            ViewData["endPage"] = endPage; // [페이징] 에 표시할 마지막 페이지

            //await writeRepository.GetFromRowAsync(fromRow, pageRows);
            var posts = await writeRepository.GetFromRowAsync(boardId ,fromRow, pageRows);
            return View(posts);
        }
    }
}
