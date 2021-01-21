using System.Collections.Generic;
using System.Linq;
using Moq;
using MovieLibrary.DTOs;
using MovieLibrary.Services;
using NUnit.Framework;

namespace MovieLibrary.Test
{
    public class MovieServiceTests
    {
        private Mock<IMovieDataFetcher> mockDataFetcher;
        private const string TheBestMovie = "The best movie";
        private const string TheWorstMovie = "The worst movie";

        [SetUp]
        public void Setup()
        {
            mockDataFetcher = new Mock<IMovieDataFetcher>();
            mockDataFetcher.Setup(o => o.GetAllMovies()).Returns(new List<Movie>
            {
                new Movie {Id = "1", Rated = 9.5, Title = TheBestMovie},
                new Movie {Id = "2", Rated = 0.5, Title = TheWorstMovie},
                new Movie {Id = "3", Rated = 6.5, Title = "An above average movie"},
                new Movie {Id = "4", Rated = 4.5, Title = "An below average movie"},
                new Movie {Id = "4", Rated = 4.5, Title = "An below average movie"},
            });
        }
        [Test]
        public void GetSortedTopList_AscendingIsTrue_ShouldReturnSortedInAscendingOrder()
        {
            //Arrange
            const string expectedFirstTitle = TheWorstMovie;
            const string expectedLastTitle = TheBestMovie;
            var movieService = new MovieService(mockDataFetcher.Object);

            //Act
            var sortedMovieList =  movieService.GetSortedTopList(ascendingOrder: true).ToList();

            //Assert
            Assert.AreEqual(expectedFirstTitle, sortedMovieList.First().Title);
            Assert.AreEqual(expectedLastTitle, sortedMovieList.Last().Title);
        }
        [Test]
        public void GetSortedTopList_AscendingIsFalse_ShouldReturnSortedInDescendingOrder()
        {
            //Arrange
            const string expectedFirstTitle = TheBestMovie;
            const string expectedLastTitle = TheWorstMovie;
            var movieService = new MovieService(mockDataFetcher.Object);

            //Act
            var sortedMovieList = movieService.GetSortedTopList(ascendingOrder: false).ToList();

            //Assert
            Assert.AreEqual(expectedFirstTitle, sortedMovieList.First().Title);
            Assert.AreEqual(expectedLastTitle, sortedMovieList.Last().Title);
        }
        [Test]
        public void RemoveDuplicates_ShouldReturnListWithCountFour()
        {
            //Arrange
            const int expectedCount = 4;
            var movieService = new MovieService(mockDataFetcher.Object);

            //Act
            var sortedMovieList = movieService.GetSortedTopList(ascendingOrder: false).ToList();
            var withOutDuplicates = movieService.RemoveDuplicates(sortedMovieList);

            //Assert
            Assert.AreEqual(expectedCount, withOutDuplicates.Count());
        }
        [Test]
        public void GetById_ShouldReturnTheBestMovie()
        {
            //Arrange
            const string expectedTitle = TheBestMovie;
            const string id = "1";
            var movieService = new MovieService(mockDataFetcher.Object);

            //Act
            var movie = movieService.GetMovieById(id);

            //Assert
            Assert.AreEqual(expectedTitle,movie.Title);
        }
    }
}