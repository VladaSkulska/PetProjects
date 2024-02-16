using HDrezka.Models;

namespace HDrezka.Services
{
    public interface IMovieService
    { 
        Task<IEnumerable<MovieDto>> GetMoviesAsync();
        Task<MovieDetailDto> GetMovieByIdAsync(int id);
        Task<MovieDto> AddMovieAsync(MovieDetailDto movieDto);
        Task UpdateMovieAsync(int id, MovieDetailDto movieDto);
        Task DeleteMovieAsync(int id);
        Task<IEnumerable<MovieDto>> FilterMoviesAsync(string genre, string title, string type);
    }
}