using HDrezka.DTOs;
using HDrezka.Models;
using HDrezka.Repositories;

namespace HDrezka.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<MovieDto>> GetMoviesAsync()
        {
            var movies = await _movieRepository.GetMoviesAsync();
            return movies.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Genre = m.Genre.ToString(),
                MovieType = m.MovieType.ToString(),
                Description = m.Description
            });
        }

        public async Task<MovieDetailsDto> GetMovieByIdAsync(int id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return null;
            }

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

        public async Task<MovieDto> AddMovieAsync(MovieDetailsDto movieDto)
        {
            if (!Enum.TryParse(movieDto.Genre, true, out MovieGenre genre))
            {
                throw new ArgumentException("Invalid genre");
            }

            if (!Enum.TryParse(movieDto.MovieType, true, out MovieType movieType))
            {
                throw new ArgumentException("Invalid movie type");
            }

            var movie = new Movie
            {
                Title = movieDto.Title,
                Genre = genre,
                MovieType = movieType,
                Description = movieDto.Description,
                DurationMinutes = movieDto.DurationMinutes,
                Director = movieDto.Director,
                ReleaseDate = movieDto.ReleaseDate
            };

            await _movieRepository.AddMovieAsync(movie);
            await _movieRepository.SaveAsync();

            return new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre.ToString(),
                MovieType = movie.MovieType.ToString(),
                Description = movie.Description
            };
        }

        public async Task UpdateMovieAsync(int id, MovieDetailsDto movieDto)
        {
            var existingMovie = await _movieRepository.GetMovieByIdAsync(id);
            if (existingMovie == null)
            {
                throw new KeyNotFoundException($"Movie with ID {id} not found");
            }

            if (!Enum.TryParse(movieDto.MovieType, true, out MovieType movieType))
            {
                throw new ArgumentException("Invalid movie type");
            }

            if (!Enum.TryParse(movieDto.Genre, true, out MovieGenre genre))
            {
                throw new ArgumentException("Invalid genre");
            }

            existingMovie.Title = movieDto.Title;
            existingMovie.ReleaseDate = movieDto.ReleaseDate;
            existingMovie.Description = movieDto.Description;
            existingMovie.MovieType = movieType;
            existingMovie.Genre = genre;
            existingMovie.DurationMinutes = movieDto.DurationMinutes;
            existingMovie.Director = movieDto.Director;

            await _movieRepository.UpdateMovieAsync(existingMovie);
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

            return movies.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Genre = m.Genre.ToString(),
                MovieType = m.MovieType.ToString(),
                Description = m.Description
            });
        }
    }
}