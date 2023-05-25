using Newtonsoft.Json;
using TeamProject.Models.Domain;

namespace TeamProject.Repositories
{
    public class TMDBService :ITMDBService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "a64af89e63ea55dc53158eca732fee02";
        private const string BaseUrl = "https://api.themoviedb.org/3/movie";

        public TMDBService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Movie> GetMovieAsync(long id)
        {
            var url = $"https://api.themoviedb.org/3/movie/{id}?api_key={ApiKey}&language=ko-KR";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Unable to retrieve data from TMDB API: {response.ReasonPhrase}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var movieData = JsonConvert.DeserializeObject<Movie>(content);

            return movieData;
        }
    }
}
