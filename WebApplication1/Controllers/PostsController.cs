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

       [HttpGet("posts/postdetail/{Id}")]
        public async Task<IActionResult> PostDetail(long id)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            var post = await writeRepository.GetPostAsync(id);
            if (post == null)
            {
                return NotFound();
            }

          if( post != null)
            {
                var comments = await writeRepository.GetIdCommentAsync(id);
                post.Comments = comments?.ToList();
            }
           

            return View(post);
        }

     
        // posts/userspostlist/userid
        [HttpGet("posts/userspostlist/{userId}")]
        public async Task<IActionResult> UsersPostList(long userId)
        {
            var posts = await writeRepository.GetUserPostAsync(userId);
            return View(posts);
        }

		[HttpPost]
		public async Task<IActionResult> Delete(long id)
		{
			var deleteUser = await writeRepository.DeletePostAsync(id);

			if (deleteUser != null)
			{
				// 삭제 성공
				return View("Delete", 1);
			}

			// 삭제 실패
			return View("Delete", 0);

		}
	}


}
