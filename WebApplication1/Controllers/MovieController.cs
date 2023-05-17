using Microsoft.AspNetCore.Mvc;
using TeamProject.Repositories;

namespace TeamProject.Controllers
{
    public class MovieController : Controller
    {
        private readonly IWriteRepository writeRepository;

        public MovieController(IWriteRepository writeRepository)
        {
            this.writeRepository = writeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        //게시글 목록 읽어오기
        [HttpGet]
        public async Task<IActionResult> List(long boardId)
        {
            var writes = await writeRepository.GetAllPostAsync(boardId);

            return View(writes);
        }

    }
}
