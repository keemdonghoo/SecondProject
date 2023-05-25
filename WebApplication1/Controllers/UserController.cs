using Microsoft.AspNetCore.Mvc;
using TeamProject.Data;
using TeamProject.Models.Domain;
using TeamProject.Models.ViewModels;
using TeamProject.Repositories;
using static Azure.Core.HttpHeader;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace TeamProject.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> MyPage(long id)
        {
            string userId = HttpContext.Session.GetString("UserId");
			id = long.Parse(userId);
            var user = await userRepository.GetAsync(id);
            return View(user);
        }
		

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await userRepository.GetAllAsync();
            return View(users);   // View(object) <- Model 로 넘어감
        }

        [HttpGet]
		public async Task<IActionResult> Detail(long id)
        {
            // 특정 id의 유저 정보
            var user = await userRepository.GetAsync(id);

            return View(user);
        }

		[HttpGet]
		public async Task<IActionResult> Update(long id)
		{
            string userId = HttpContext.Session.GetString("UserId");
			id = long.Parse(userId);	

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
		public async Task<IActionResult> Update(EditUserRequest request)
		{
			// Validation
			request.Validate();
			if (request.HasError)
			{
				TempData["NameError"] = request.ErrorName;
				TempData["PassWordError"] = request.ErrorPassWord;
				TempData["UserNameError"] = request.ErrorUserName;
				TempData["PhoneNumError"] = request.ErrorPhoneNum;
				TempData["NickNameError"] = request.ErrorNickName;
				TempData["EmailError"] = request.ErrorEmail;

				// 사용자가 입력했던 데이터
				TempData["Name"] = request.Name;
				TempData["PassWord"] = request.PassWord;
				TempData["UserName"] = request.UserName;
				TempData["PhoneNum"] = request.PhoneNum;
				TempData["NickName"] = request.NickName;
				TempData["Email"] = request.Email;

				return RedirectToAction("Update");
			}

			var user = new User
			{
				Id = request.UserId,
				Name = request.Name,
				PassWord = request.PassWord,
				UserName = request.UserName,
				PhoneNum = request.PhoneNum,
				NickName = request.NickName,
				Email = request.Email,
			};
			var updateWrite = await userRepository.UpdateAsync(user);

			if (updateWrite == null)
			{
				// 수정 실패하면 List 로
				return RedirectToAction("Update");
			}
            TempData["UpdateSuccess"] = true;
            return RedirectToAction("Update", new { id = request.UserId });

			

		}

		[HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var deleteUser = await userRepository.DeleteAsync(id);

            if (deleteUser != null)
            {
                // 삭제 성공
                return View("DeleteOk", 1);
            }

            // 삭제 실패
            return View("DeleteOk", 0);

        }



    }
}
