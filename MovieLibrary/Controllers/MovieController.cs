using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.DTOs;
using MovieLibrary.Services;

namespace MovieLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService movieService;

        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        [Route("/toplist")]
        public ActionResult<IEnumerable<string>> GetMovieTopListTitles(bool ascending = true)
        {
            try
            {
                var movies = movieService.GetSortedTopList(ascending);

                return Ok(movies.Select(m => m.Title));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
        
        [HttpGet]
        [Route("/movie")]
        public ActionResult<Movie> GetMovieById(string id)
        {
            try
            {
                var movie = movieService.GetMovieById(id);
                return Ok(movie);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}