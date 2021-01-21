using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MovieLibrary.DTOs
{
    public class DetailedMovie
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("imdbRating")]
        public decimal ImdbRating { get; set; }
    }
}

