using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;
using TeamProject.Data;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace TeamProject.Controllers
{
    public class SearchController : Controller
    {
        private readonly MovieDbContext _dbContext;

        public SearchController(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
