using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using TeamProject.Data;
using TeamProject.Models.Domain;
using TeamProject.Models.ViewModels;
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
        public async Task<IActionResult> Create(CreatePostViewModel createPostViewModel)
        {
            // Validation
            createPostViewModel.ErrorCheck();
            if (createPostViewModel.HasError)
            {
                TempData["TitleError"] = createPostViewModel.ErrorTitle;
                TempData["ContentError"] = createPostViewModel.ErrorContent;

                // 사용자가 입력했던 데이터
                TempData["Title"] = createPostViewModel.Title;
                TempData["Content"] = createPostViewModel.Content;

                return View(createPostViewModel);
            }
            var post = new Post
            {
                Title = createPostViewModel.Title,
                Content = createPostViewModel.Content,
                Date = createPostViewModel.Date,
                ViewCnt = createPostViewModel.ViewCnt,
                LikeCnt = createPostViewModel.LikeCnt,
                UserId = createPostViewModel.UserId,
                BoardId = 1,
            };

            post = await writeRepository.AddPostAsync(post);

            return RedirectToAction("PostDetail", new { id = post.Id });
        }

       [HttpGet("posts/postdetail/{Id}")]
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
                return View("Delete", 1);
            }

            return View("Delete", 0);
        }


        // posts/userspostlist/userid
        [HttpGet("posts/userspostlist/{userId}")]
        public async Task<IActionResult> UsersPostList(long userId)
        {
            var posts = await writeRepository.GetUserPostAsync(userId);
            return View(posts);
        }
    }


}
