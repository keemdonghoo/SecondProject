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

        //public ActionResult SearchMovie(string title)
        //{
        //    if (string.IsNullOrWhiteSpace(title))
        //    {
        //        return BadRequest("Title cannot be null or whitespace.");
        //    }

        //    var movie = _dbContext.Movies
        //        .FirstOrDefault(m => m.Title.ToLower().Contains(title.ToLower()));

        //    if (movie == null)
        //    {
        //        return NotFound("No movie found with the given title.");
        //    }

        //    return Json(movie);
        //}

    }
}
