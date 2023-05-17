using Microsoft.AspNetCore.Mvc;
using TeamProject.Data;
using TeamProject.Repositories;

namespace TeamProject.Controllers
{
    [Route("[action]")]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

		[HttpGet("/User/Detail/{id}")]
		public async Task<IActionResult> Detail(long id)
        {
            // 특정 id의 유저 정보
            var user = await userRepository.GetAsync(id);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var deleteCoupon = await userRepository.DeleteAsync(id);

            if (deleteCoupon != null)
            {
                // 삭제 성공
                return View("DeleteOk", 1);
            }

            // 삭제 실패
            return View("DeleteOk", 0);

        }



    }
}
