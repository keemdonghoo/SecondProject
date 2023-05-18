using Microsoft.AspNetCore.Mvc;
using TeamProject.Models.Domain;
using TeamProject.Models.ViewModels;
using TeamProject.Repositories;
namespace TeamProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IWriteRepository writeRepository;
        public LoginController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            AddUserRequest addUserRequest = new()
            {
                Name = (string)TempData["Name"],
                PassWord = (string)TempData["PassWord"],
                UserName = (string)TempData["UserName"],
                PhoneNum = (string)TempData["PhoneNum"],
                NickName = (string)TempData["NickName"],
                Email = (string)TempData["Email"],
            };
            //ViewData["page"] = HttpContext.Session.GetInt32("page") ?? 1;

            return View(addUserRequest);
        }
        [HttpPost]
        [ActionName("SignUp")]
        public async Task<IActionResult> Add(AddUserRequest addUserRequest)
        {
            addUserRequest.Validate();
            if (addUserRequest.HasError)
            {
                TempData["NameError"] = addUserRequest.ErrorName;
                TempData["PassWordError"] = addUserRequest.ErrorPassWord;
                TempData["UserNameError"] = addUserRequest.ErrorUserName;
                TempData["PhoneNumError"] = addUserRequest.ErrorPhoneNum;
                TempData["NickNameError"] = addUserRequest.ErrorNickName;

                TempData["Name"] = addUserRequest.Name;
                TempData["PassWord"] = addUserRequest.PassWord;
                TempData["UserName"] = addUserRequest.UserName;
                TempData["PhoneNum"] = addUserRequest.PhoneNum;
                TempData["NickName"] = addUserRequest.NickName;
                TempData["Email"] = addUserRequest.Email;

                return RedirectToAction("SignUp");
            }

            var user = new User
            {
                Name = addUserRequest.Name,
                PassWord = addUserRequest.PassWord,
                UserName = addUserRequest.UserName,
                PhoneNum = addUserRequest.PhoneNum,
                NickName = addUserRequest.NickName,
                Email = addUserRequest.Email,                

            };

            user = await userRepository.AddAsync(user);
            return RedirectToAction("Detail", new { id = user.Id });
        }
    }
}
