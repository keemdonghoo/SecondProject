using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TeamProject.Models.Domain;

namespace TeamProject.Repositories
{
    public interface ITMDBService
    {
        Task<Movie> GetMovieAsync(long id);
    }
}
