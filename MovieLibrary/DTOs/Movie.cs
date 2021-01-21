using Newtonsoft.Json;

namespace MovieLibrary.DTOs
{
    public class Movie
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("rated")]
        public decimal Rated { get; set; }
    }
}