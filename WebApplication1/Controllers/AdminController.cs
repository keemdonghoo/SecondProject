using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TeamProject.Models.Domain;
using TeamProject.Repositories;

namespace TeamProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IWriteRepository writeRepository;

        public AdminController(IUserRepository userRepository,IWriteRepository writeRepository)
        {
            this.userRepository = userRepository;
            this.writeRepository = writeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> AdminPage()
        {
            return View();
        }

        [HttpGet("admin/UserList")]
        public async Task<IActionResult> UserList()
        {
            var users = await userRepository.GetAllAsync();
            return View(users);
        }

        [HttpGet("admin/PostList")]
        public async Task<IActionResult> PostList()
        {
            var posts = await writeRepository.AdminGetAllPostAsync();
            return View(posts);
        }

        [HttpGet("admin/CommentList")]
        public async Task<IActionResult> CommentList()
        {
            var comments = await writeRepository.AdminGetAllCommentAsync();
            return View(comments);
        }


    }
}
