using Cibertec.Shopping.WEB.Areas.Movies.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Cibertec.Shopping.WEB.Areas.Movies.Controllers
{
    [Area(nameof(Movies))]
    public class HomeController : Controller
    {
        //TODO: Llevar al appsettings
        private readonly string apiKey = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJjODRmMmUwODYxM2JmN2FlYTM1OGI0OTgzNDNkMWUwNiIsInN1YiI6IjVmZTUxM2M3ZTE4Yjk3MDAzYzg5NzE3MCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.StJwmra-PsBwZTOXWg3Y06VEu8CGfAo8dXV7ZQ3RnIs";
        private const int MaxPages = 20;
        public IActionResult Index(int page = 1)
        {
            var movies = GetAllMovies(page);
            ViewBag.MaxPages = MaxPages;
            return View(movies);
        }

        public IActionResult Favorites(int page = 1)
        {
            var favoriteMovies = GetAllMovies(page, true);
            ViewBag.MaxPages = MaxPages;
            ViewBag.IsFavorite = favoriteMovies.Any();
            return View("Index", favoriteMovies);
        }

        private List<MovieViewModel> GetAllMovies(int page, bool isFavorite = false)
        {
            var movies = new List<MovieViewModel>();

            using HttpClient httpClient = new HttpClient();

            var endpointUrl = isFavorite
                            ? $"https://api.themoviedb.org/3/account/9943299/favorite/movies?language=en-US&page={page}&sort_by=created_at.asc"
                            : $"https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=en-US&page={page}&sort_by=popularity.desc";

            var authToken = "Bearer " + apiKey;

            httpClient.DefaultRequestHeaders.Add("Authorization", authToken);

            using HttpResponseMessage response = httpClient.GetAsync(endpointUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                using HttpContent content = response.Content;

                string jsonResult = content.ReadAsStringAsync().Result;

                JsonSerializerOptions jsonOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                };

                movies = JsonSerializer.Deserialize<MovieApiResult>(jsonResult, jsonOptions)?.Results;

            }

            return movies;
        }
    }
}
