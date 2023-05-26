using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TeamProject.Models.Domain;
using TeamProject.Models.ViewModels;
using TeamProject.Repositories;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace TeamProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IWriteRepository writeRepository;

        public AdminController(IUserRepository userRepository, IWriteRepository writeRepository)
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

        [HttpGet]
        public async Task<IActionResult> Update(long id)
        {


            var user = await userRepository.GetAsync(id);
            if (user == null) return View(null);

            EditUserRequest userrequest = new()
            {
                UserId = user.Id,
                Name = (string)TempData["Name"] ?? user.Name,
                PassWord = (string)TempData["PassWord"] ?? user.PassWord,
                UserName = (string)TempData["UserName"] ?? user.UserName,
                PhoneNum = (string)TempData["PhoneNum"] ?? user.PhoneNum,
                NickName = (string)TempData["NickName"] ?? user.NickName,
                Email = (string)TempData["Email"] ?? user.Email,
            };

            return View(userrequest);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSelectedUsers(string selectedUserIds)
        {
            if (!string.IsNullOrEmpty(selectedUserIds))
            {
                var userIds = selectedUserIds.Split(',')
                    .Select(id => long.TryParse(id, out long userId) ? userId : 0)
                    .Where(userId => userId != 0)
                    .ToList();

                await userRepository.DeleteSelectedUsers(userIds);
                return View("DeleteUser", 1);
            }

            return View("DeleteUser", 0);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSelectedPosts(string selectedPostIds)
        {
            if (!string.IsNullOrEmpty(selectedPostIds))
            {
                var postIds = selectedPostIds.Split(',')
                    .Select(id => long.TryParse(id, out long postId) ? postId : 0)
                    .Where(postId => postId != 0)
                    .ToList();

                await writeRepository.DeleteSelectedPosts(postIds);
                return View("DeletePost", 1);
            }

            return View("DeletePost", 0);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteSelectedComments(string selectedCommentIds)
        {
            if (!string.IsNullOrEmpty(selectedCommentIds))
            {
                var commentIds = selectedCommentIds.Split(',')
                    .Select(id => long.TryParse(id, out long commentId) ? commentId : 0)
                    .Where(commentId => commentId != 0)
                    .ToList();

                await writeRepository.DeleteSelectedComments(commentIds);
                return View("DeleteComment", 1);
            }

            return View("DeleteComment", 0);
        }
    }




}
