using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Options;
using MovieLibrary.DTOs;
using Newtonsoft.Json;

namespace MovieLibrary.Services
{
    public class MovieDataFetcher : IMovieDataFetcher
    {
        private readonly string detailedMoviesUrl;
        private readonly string top100Url;

        public MovieDataFetcher(IOptions<EndpointOptions> options)
        {
            detailedMoviesUrl = options.Value.DetailedMovies;
            top100Url = options.Value.Top100;
        }
        public Movie GetMovieById(string id)
        {
            return null;

        }

        public IEnumerable<Movie> GetAllMovies()
        {
            var moviesFromTop100 = GetMoviesFromTop100();
            var moviesFromDetailedMovies = GetMoviesFromDetailedMovies();
            var allMovies = moviesFromTop100.Concat(moviesFromDetailedMovies);
            return allMovies;
        }

        private IEnumerable<Movie> GetMoviesFromDetailedMovies()
        {
            var client = new HttpClient();

            var httpResponse = client.GetAsync(detailedMoviesUrl).Result;
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Something went wrong when fetching the movie data");
            }

            var content = httpResponse.Content.ToString();
            if (content is null)
            {
                throw new Exception("No data in response from external API");
            }
            var json = new StreamReader(httpResponse.Content.ReadAsStream()).ReadToEnd();

            var movies = JsonConvert.DeserializeObject<List<Movie>>(json, new JsonSerializerSettings
            {
                Culture = new System.Globalization.CultureInfo("sv-SE")
            });
            return movies;
        }

        private IEnumerable<Movie> GetMoviesFromTop100()
        {
            var client = new HttpClient();

            var httpResponse = client.GetAsync(top100Url).Result;
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Something went wrong when fetching the movie data");
            }


            var content = httpResponse.Content.ReadAsStream().ToString();
            if (content is null)
            {
                throw new Exception("No data in response from external API");
            }
            var json = new StreamReader(httpResponse.Content.ReadAsStream()).ReadToEnd();

            var detailedMovies = JsonConvert.DeserializeObject<List<DetailedMovie>>(json);
            var movies = MapDetailedMoviesToMovieDTO(detailedMovies);
            return movies;
        }

        private IEnumerable<Movie> MapDetailedMoviesToMovieDTO(List<DetailedMovie> detailedMovies)
        {
            return detailedMovies.Select(m => new Movie {Id = m.Id, Rated = m.ImdbRating, Title = m.Title});
        }
    }
}
