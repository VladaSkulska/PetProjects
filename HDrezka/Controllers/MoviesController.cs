using HDrezka.Models.DTOs;
using HDrezka.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HDrezka.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieService.GetMoviesAsync();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie(MovieDetailsDto movieDto)
        {
            try
            {
                var addedMovie = await _movieService.AddMovieAsync(movieDto);
                return CreatedAtAction(nameof(GetMovie), new { id = addedMovie.Id }, addedMovie);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, MovieDetailsDto movieDto)
        {
            try
            {
                await _movieService.UpdateMovieAsync(id, movieDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                await _movieService.DeleteMovieAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterMovies(string? genre = null, string? title=null, string? type = null)
        {
            var movies = await _movieService.FilterMoviesAsync(genre, title, type);

            return Ok(movies);
        }

        [HttpGet("{id}/description")]
        public async Task<IActionResult> GetMovieDescription(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie.Description);
        }
    }
}