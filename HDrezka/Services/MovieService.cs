using AutoMapper;
using HDrezka.Models;
using HDrezka.Models.DTOs;
using HDrezka.Repositories.Interfaces;
using HDrezka.Services.Interfaces;
using HDrezka.Utilities.Validation;
using System.ComponentModel.DataAnnotations;

namespace HDrezka.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMapper _movieMapper;
        private readonly IMovieRepository _movieRepository;

        private readonly MovieDetailsDtoValidator _movieValidator;
        public MovieService(IMovieRepository movieRepository, IMapper movieMapper, MovieDetailsDtoValidator movieValidator)
        {
            _movieRepository = movieRepository;
            _movieMapper = movieMapper;
            _movieValidator = movieValidator;
        }

        public async Task<IEnumerable<MovieDto>> GetMoviesAsync()
        {
            var movies = await _movieRepository.GetMoviesAsync();
            return _movieMapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public async Task<MovieDetailsDto> GetMovieByIdAsync(int id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return null;
            }

            return _movieMapper.Map<MovieDetailsDto>(movie);
        }

        public async Task<MovieDto> AddMovieAsync(MovieDetailsDto movieDto)
        {
            await ValidateMovieDetailsDtoAsync(movieDto);

            var movie = _movieMapper.Map<Movie>(movieDto);

            await _movieRepository.AddMovieAsync(movie);
            await _movieRepository.SaveAsync();

            return _movieMapper.Map<MovieDto>(movie);
        }

        public async Task UpdateMovieAsync(int id, MovieDetailsDto movieDto)
        {
            await ValidateMovieDetailsDtoAsync(movieDto);
            var existingMovie = await _movieRepository.GetMovieByIdAsync(id);
            if (existingMovie == null)
            {
                throw new KeyNotFoundException($"Movie with ID {id} not found");
            }

            _movieMapper.Map(movieDto, existingMovie);

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

            return _movieMapper.Map<IEnumerable<MovieDto>>(movies);
        }

        private async Task ValidateMovieDetailsDtoAsync(MovieDetailsDto movieDto)
        {
            var validationResult = await _movieValidator.ValidateAsync(movieDto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validation failed: {errors}");
            }
        }
    }
}