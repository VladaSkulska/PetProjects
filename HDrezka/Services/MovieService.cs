using HDrezka.Models;
using HDrezka.Models.DTOs;
using HDrezka.Repositories;
using HDrezka.Utilities.Validation;
using System.ComponentModel.DataAnnotations;

namespace HDrezka.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        private readonly MovieDetailsDtoValidator _movieValidator;

        public MovieService(IMovieRepository movieRepository, MovieDetailsDtoValidator movieValidator)
        {
            _movieRepository = movieRepository;
            _movieValidator = movieValidator;
        }

        public async Task<IEnumerable<MovieDto>> GetMoviesAsync()
        {
            var movies = await _movieRepository.GetMoviesAsync();
            return movies.Select(MapToDto);
        }

        public async Task<MovieDetailsDto> GetMovieByIdAsync(int id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return null;
            }

            return MapToDetailsDto(movie);
        }

        public async Task<MovieDto> AddMovieAsync(MovieDetailsDto movieDto)
        {
            await ValidateMovieDetailsDtoAsync(movieDto);

            var (genre, movieType) = ParseValues(movieDto);

            var movie = MapToEntity(genre, movieType, movieDto);

            await _movieRepository.AddMovieAsync(movie);
            await _movieRepository.SaveAsync();

            return MapToDto(movie);
        }

        public async Task UpdateMovieAsync(int id, MovieDetailsDto movieDto)
        {
            await ValidateMovieDetailsDtoAsync(movieDto);

            var (genre, movieType) = ParseValues(movieDto);

            var existingMovie = await _movieRepository.GetMovieByIdAsync(id);
            if (existingMovie == null)
            {
                throw new KeyNotFoundException($"Movie with ID {id} not found");
            }

            MapToEntity(genre, movieType, movieDto, existingMovie);

            await _movieRepository.SaveAsync();
        }

        public async Task DeleteMovieAsync(int id)
        {
            var existingMovie = await _movieRepository.GetMovieByIdAsync(id);
            if (existingMovie == null)
            {
                throw new KeyNotFoundException($"Movie with ID {id} not found");
            }

            await _movieRepository.DeleteMovieAsync(id);
            await _movieRepository.SaveAsync();
        }

        public async Task<IEnumerable<MovieDto>> FilterMoviesAsync(string genre, string title, string type)
        {
            var movies = await _movieRepository.GetMoviesAsync();

            if (!string.IsNullOrEmpty(genre))
            {
                movies = movies.Where(m => m.Genre.ToString().Equals(genre, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(title))
            {
                movies = movies.Where(m => m.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(type))
            {
                movies = movies.Where(m => m.MovieType.ToString().Equals(type, StringComparison.OrdinalIgnoreCase));
            }

            return movies.Select(MapToDto);
        }

        private async Task ValidateMovieDetailsDtoAsync(MovieDetailsDto movieDto)
        {
            var validationResult = await new MovieDetailsDtoValidator().ValidateAsync(movieDto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validation failed: {errors}");
            }
        }

        private (MovieGenre Genre, MovieType MovieType) ParseValues(MovieDetailsDto movieDto)
        {
            var genre = Enum.Parse<MovieGenre>(movieDto.Genre, true);
            var movieType = Enum.Parse<MovieType>(movieDto.MovieType, true);
            
            return (genre, movieType);
        }

        private MovieDto MapToDto(Movie movie)
        {
            return new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre.ToString(),
                MovieType = movie.MovieType.ToString(),
                Description = movie.Description
            };
        }

        private MovieDetailsDto MapToDetailsDto(Movie movie)
        {
            return new MovieDetailsDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre.ToString(),
                MovieType = movie.MovieType.ToString(),
                Description = movie.Description,
                DurationMinutes = movie.DurationMinutes,
                Director = movie.Director,
                ReleaseDate = movie.ReleaseDate
            };
        }

        private Movie MapToEntity(MovieGenre genre, MovieType movieType, MovieDetailsDto movieDto)
        {
            return new Movie
            {
                Title = movieDto.Title,
                Genre = genre,
                MovieType = movieType,
                Description = movieDto.Description,
                DurationMinutes = movieDto.DurationMinutes,
                Director = movieDto.Director,
                ReleaseDate = movieDto.ReleaseDate
            };
        }

        private void MapToEntity(MovieGenre genre, MovieType movieType, MovieDetailsDto movieDto, Movie movie)
        {
            movie.Title = movieDto.Title;
            movie.Genre = genre;
            movie.MovieType = movieType;
            movie.Description = movieDto.Description;
            movie.DurationMinutes = movieDto.DurationMinutes;
            movie.Director = movieDto.Director;
            movie.ReleaseDate = movieDto.ReleaseDate;
        }
    }
}