using Microsoft.AspNetCore.Mvc;
using TeamProject.Data;
using TeamProject.Models.Domain;
using TeamProject.Models.ViewModels;
using TeamProject.Repositories;
namespace TeamProject.Controllers
{
    //
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
        public IActionResult SignUp(AddUserRequest addUserRequest)
        {
            addUserRequest = new()
            {
                Name = (string)TempData["Name"],
                NameCheck = (bool?)TempData["NameCheck"],
                PassWord = (string)TempData["PassWord"],
                PassWordCheck = (string)TempData["PassWordCheck"],
                UserName =(string)TempData["UserName"],
                PhoneNum = (string)TempData["PhoneNum"],
                NickName = (string)TempData["NickName"],
                Email = (string)TempData["Email"],
            };
            
            //ViewData["page"] = HttpContext.Session.GetInt32("page") ?? 1;

            return View(addUserRequest);
        }

        [HttpGet("CheckId")]
        public async Task<IActionResult> CheckId(string id, AddUserRequest addUserRequest)
        {
            
            
            var isExsist = await userRepository.GetByNameAsync(id);
            if (isExsist == null)
            {
                //addUserRequest.NameCheck = true;
                //TempData["NameCheck"] = addUserRequest.NameCheck;
                return Ok(true);
            }
            return Ok(false) ;
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
                TempData["EmailError"] = addUserRequest.ErrorEmail;

                TempData["Name"] = addUserRequest.Name;
                TempData["NameCheck"] = addUserRequest.NameCheck;
                TempData["PassWord"] = addUserRequest.PassWord;
                TempData["PassWordCheck"] = addUserRequest.PassWordCheck;
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
            return RedirectToAction("Login");
        }
    }
}
