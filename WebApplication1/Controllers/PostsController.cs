using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamProject.Data;
using TeamProject.Models.Domain;
using TeamProject.Repositories;

namespace TeamProject.Controllers
{
    //게시글 컨트롤러
    public class PostsController : Controller
    {
        private readonly IWriteRepository writeRepository;

        public PostsController(IWriteRepository writeRepository)
        {
            this.writeRepository = writeRepository;
        }

        // GET: Posts/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,Date,ViewCnt,LikeCnt,UserId,BoardId")] Post post)
        {
            if (ModelState.IsValid)
            {
                await writeRepository.AddPostAsync(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Details/5
        [HttpGet]
        public async Task<IActionResult> PostDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = await writeRepository.GetPostAsync(id.Value);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var deletedWrite = await writeRepository.DeletePostAsync(id);

            if (deletedWrite != null)
            {
                // 삭제 성공
                return View("Delete", 1);  // View(string, object) => viewname, model
            }

            ViewData["page"] = HttpContext.Session.GetInt32("page") ?? 1;

            // 삭제 실패
            return View("Delete", 0);
        }


    }


}
