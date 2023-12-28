using System.Text.Json.Serialization;

namespace Cibertec.Shopping.WEB.Areas.Movies.Models
{
    public class MovieApiResult
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }
        [JsonPropertyName("results")]
        public List<MovieViewModel> Results { get; set; }
    }
}
