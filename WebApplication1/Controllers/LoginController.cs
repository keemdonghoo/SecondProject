using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repositories;
using WebApplication1.Models.ViewMoldels;
using WebApplication1.Models.Domain;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository userRepository;
        public LoginController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IActionResult SignUp()
        {
            AddUserRequest addUserRequest = new()
            {
                UserName = (string)TempData["UserName"],
                PhoneNum = (string)TempData["PhoneNum"],
                Name = (string)TempData["Name"],
                PassWord = (string)TempData["PassWord"],
                Email = (string)TempData["Email"],
                NickName = (string)TempData["NickName"],
            };
                        
            return View(addUserRequest);
        }
        [HttpPost]
        [ActionName("SignUp")]
        public async Task<IActionResult> Add(AddUserRequest addUserRequest)
        {
            var user = new User
            {
                UserName = addUserRequest.UserName,
                PhoneNum = addUserRequest.PhoneNum,
                Name = addUserRequest.Name,
                PassWord = addUserRequest.PassWord,
                Email = addUserRequest.Email,
                NickName= addUserRequest.NickName,
            };
            user = await userRepository.AddAsync(user);
            return RedirectToAction("Detail", new { id = user.UserUID });
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var user = await userRepository.GetAllAsync();
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(long id)
        {
            var user = await userRepository.DeleteAsync(id);
            if (user == null)
            {
                return RedirectToAction("List");
            }
            return View(user);
        }
    }
}
