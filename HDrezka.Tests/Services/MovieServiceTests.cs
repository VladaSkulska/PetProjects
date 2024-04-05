using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using HDrezka.Models.DTOs;
using HDrezka.Models;
using HDrezka.Repositories.Interfaces;
using HDrezka.Services;
using HDrezka.Utilities.Validation;
namespace HDrezka.Tests.Services
{
    public class MovieServiceTests
    {
        private IMovieRepository _movieRepository;
        private IMapper _mapper;
        private MovieDetailsDtoValidator _validator;

        public MovieServiceTests()
        {
            _movieRepository = A.Fake<IMovieRepository>();
            _mapper = A.Fake<IMapper>();
            _validator = A.Fake<MovieDetailsDtoValidator>();
        }

        [Fact]
        public async Task GetMoviesAsync_WhenMoviesExist_ReturnsListOfMovieDtos()
        {
            // Arrange
            var movieService = new MovieService(_movieRepository, _mapper, null);

            var movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Movie 1" },
                new Movie { Id = 2, Title = "Movie 2" }
            };

            A.CallTo(() => _movieRepository.GetMoviesAsync()).Returns(movies);

            var expectedDtos = _mapper.Map<IEnumerable<MovieDto>>(movies);

            // Act
            var result = await movieService.GetMoviesAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedDtos);
        }

        [Fact]
        public async Task GetMovieByIdAsync_WhenMovieExists_ReturnsMovieDetailsDto()
        {
            // Arrange
            var movieService = new MovieService(_movieRepository, _mapper, _validator);

            var movieId = 1; 
            var movie = new Movie { Id = movieId, Title = "Test Movie" };
            var movieDetailsDto = new MovieDetailsDto { Id = movieId, Title = movie.Title };

            A.CallTo(() => _movieRepository.GetMovieByIdAsync(movieId)).Returns(movie);
            A.CallTo(() => _mapper.Map<MovieDetailsDto>(movie)).Returns(movieDetailsDto);

            // Act
            var result = await movieService.GetMovieByIdAsync(movieId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(movieId);
            result.Title.Should().Be(movie.Title);
        }

        [Fact]
        public async Task GetMovieByIdAsync_WhenMovieDoesNotExist_ReturnsNull()
        {
            // Arrange
            var movieService = new MovieService(_movieRepository, _mapper, null);

            var nonExistingMovieId = 999;
            A.CallTo(() => _movieRepository.GetMovieByIdAsync(nonExistingMovieId)).Returns(Task.FromResult<Movie>(null));

            // Act
            var result = await movieService.GetMovieByIdAsync(nonExistingMovieId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateMovieAsync_WhenValidMovieDetails_ReturnsTask()
        {
            // Arrange
            var movieService = new MovieService(_movieRepository, _mapper, _validator);

            var movieDto = new MovieDetailsDto { Id = 1, Title = "Updated Movie" };
            var existingMovie = new Movie { Id = movieDto.Id, Title = "Existing Movie" };

            A.CallTo(() => _movieRepository.GetMovieByIdAsync(movieDto.Id)).Returns(existingMovie);

            // Act
            Func<Task> act = async () => await movieService.UpdateMovieAsync(movieDto.Id, movieDto);

            // Assert
            await act.Should().NotThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task DeleteMovieAsync_WhenMovieExists_ReturnsTask()
        {
            // Arrange
            var movieService = new MovieService(_movieRepository, _mapper, _validator);

            var existingMovie = new Movie { Id = 1, Title = "Existing Movie" };

            A.CallTo(() => _movieRepository.GetMovieByIdAsync(existingMovie.Id)).Returns(existingMovie);

            // Act
            Func<Task> act = async () => await movieService.DeleteMovieAsync(existingMovie.Id);

            // Assert
            await act.Should().NotThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task FilterMoviesAsync_WhenMoviesExist_ReturnsFilteredMovieDtos()
        {
            // Arrange
            var movieService = new MovieService(_movieRepository, _mapper, _validator);

            var movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Movie 1", Genre = MovieGenre.Action, MovieType = MovieType.Movie },
                new Movie { Id = 2, Title = "Movie 2", Genre = MovieGenre.Drama, MovieType = MovieType.Movie },
                new Movie { Id = 3, Title = "Movie 3", Genre = MovieGenre.Action, MovieType = MovieType.Documentary },
            };

            A.CallTo(() => _movieRepository.GetMoviesAsync()).Returns(Task.FromResult<IEnumerable<Movie>>(movies));

            var genre = "Action";
            var title = "Movie";
            var type = "Movie";

            var expectedFilteredMovies = movies
                .Where(m => m.Genre.ToString().Equals(genre, StringComparison.OrdinalIgnoreCase))
                .Where(m => m.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                .Where(m => m.MovieType.ToString().Equals(type, StringComparison.OrdinalIgnoreCase));

            var expectedDtos = expectedFilteredMovies.Select(movie => new MovieDto { Id = movie.Id, Title = movie.Title });

            A.CallTo(() => _mapper.Map<IEnumerable<MovieDto>>(A<IEnumerable<Movie>>._)).Returns(expectedDtos);

            // Act
            var result = await movieService.FilterMoviesAsync(genre, title, type);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedDtos);
        }
    }
}