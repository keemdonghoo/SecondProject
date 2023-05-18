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
        private readonly MovieDbContext movieDbContext;

        public PostsController(MovieDbContext movieDbContext)
        {
            this.movieDbContext = movieDbContext;
        }

        // GET: Posts/Create
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
                movieDbContext.Add(post);
                await movieDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> PostDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //FirstOrDefaultAsync
            var post = await movieDbContext.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // ...
    }

}
