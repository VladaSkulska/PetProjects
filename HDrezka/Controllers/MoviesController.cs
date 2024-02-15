using HDrezka.Models;
using HDrezka.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HDrezka.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : Controller
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieRepository.GetMoviesAsync();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);   
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie(Movie movie)
        {
            await _movieRepository.AddMovieAsync(movie);
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            var existingMovie = _movieRepository.GetMovieByIdAsync(id);
            if (existingMovie == null)
            {
                return NotFound();
            }

            await _movieRepository.UpdateMovieAsync(movie);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = _movieRepository.DeleteMovieAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            await _movieRepository.DeleteMovieAsync(id);
            return NoContent();
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterMovies(string genre, string name, string type)
        {
            var movies = await _movieRepository.GetMoviesAsync();

            if (!string.IsNullOrEmpty(genre))
            {
                movies = movies.Where(m => m.Genre.ToString().Equals(genre, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(name))
            {
                movies = movies.Where(m => m.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(type))
            {
                movies = movies.Where(m => m.MovieType.ToString().Equals(type, StringComparison.OrdinalIgnoreCase));
            }

            return Ok(movies);
        }

        [HttpGet("{id}/description")]
        public async Task<IActionResult> GetMovieDescription(int id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie.Description);
        }
    }
}