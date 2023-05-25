using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamProject.Data;
using TeamProject.Models.Domain;
using System;
using TeamProject.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace MyMovieApp.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieDbContext _context;
        private readonly ITMDBService _tmdbService;  // Assuming you have a service for TMDB API

        public MovieController(MovieDbContext context, ITMDBService tmdbService)
        {
            _context = context;
            _tmdbService = tmdbService;
        }

        //[HttpGet]
        //[Route("Movie/Detail")]
        //public async Task<IActionResult> Detail(long id)
        //{
        //    try
        //    {
        //        var movie = await _context.Movies.FindAsync(id);

        //        if (movie == null)
        //        {
        //            var movieData = await _tmdbService.GetMovieAsync(id);

        //            if (movieData == null)
        //            {
        //                return NotFound();
        //            }

        //            movie = new Movie
        //            {
        //                MovieUid = movieData.Id,
        //                RateAvg = 0.0f,
        //            };

        //            _context.Movies.Add(movie);
        //            await _context.SaveChangesAsync();
        //        }

        //        return View(movie);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        public class MovieData
        {
            public long tmdbid { get; set; }
            public string tmdbTitle { get; set; }
        }


        [HttpPost]
        [Route("Movie/EnsureMovieInDatabase")]
        public async Task<IActionResult> EnsureMovieInDatabase([FromBody] MovieData data)

        {
            if (data.tmdbid == 0 || string.IsNullOrEmpty(data.tmdbTitle))
            {
                return BadRequest("tmdbid or tmdbTitle is not provided properly.");
            }

            try
            {
                // 데이터베이스에서 해당 ID를 가진 영화를 찾습니다.
                var movie = _context.Movies.FirstOrDefault(m => m.MovieUid == data.tmdbid);
                
                // 영화가 없다면, TMDB API를 호출하여 영화 정보를 가져옵니다.
                if (movie == null)
                {
                    var movieData = await _tmdbService.GetMovieAsync(data.tmdbid);

                    // 데이터베이스에 영화 정보를 저장합니다.
                    movie = new Movie
                    {
                        //Id = movieData.Id,
                        MovieUid = movieData.Id,
                        Title = movieData.Title,
                        RateAvg = 0.0f,
                    };

                    _context.Movies.Add(movie);
                    await _context.SaveChangesAsync();
                }

                // Detail 페이지로의 URL을 응답에 포함시킵니다.
                var detailUrl = Url.Action("Detail", "Home", new { title = data.tmdbTitle });
                return Json(new { detailUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
