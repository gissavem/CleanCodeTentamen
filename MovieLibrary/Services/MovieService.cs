using System;
using System.Collections.Generic;
using System.Linq;
using MovieLibrary.DTOs;

namespace MovieLibrary.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieDataFetcher dataFetcher;

        public MovieService(IMovieDataFetcher dataFetcher)
        {
            this.dataFetcher = dataFetcher;
        }

        public IEnumerable<Movie> GetSortedTopList(bool ascendingOrder = true)
        {
            var allMovies = dataFetcher.GetAllMovies();

            var sortedList = ascendingOrder 
                ? allMovies.OrderBy(m => m.Rated).ToList() 
                : allMovies.OrderByDescending(m => m.Rated).ToList();

            var withoutDuplicates = RemoveDuplicates(sortedList);

            return withoutDuplicates;
        }

        public IEnumerable<Movie> RemoveDuplicates(IEnumerable<Movie> movies)
        {
            return movies
                .GroupBy(m => m.Title)
                .Select(g => g.First());
        }

        public Movie GetMovieById(string id)
        {
            var allMovies = dataFetcher.GetAllMovies();
            var movie = allMovies.FirstOrDefault(m => m.Id == id);
            if (movie is null)
            {
                throw new KeyNotFoundException($"Movie with id: \"{id}\" not found");
            }

            return movie;
        }
    }
}
