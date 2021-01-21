using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieLibrary.DTOs;

namespace MovieLibrary.Services
{
    public interface IMovieService
    {
        public IEnumerable<Movie> GetSortedTopList(bool ascendingOrder = true);
        public IEnumerable<Movie> RemoveDuplicates(IEnumerable<Movie> movies);
        public Movie GetMovieById(string id);
    }
}
