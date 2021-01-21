using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieLibrary.DTOs;

namespace MovieLibrary.Services
{
    public interface IMovieDataFetcher
    {
        public IEnumerable<Movie> GetAllMovies();

        public Movie GetMovieById(string id);
    }
}
