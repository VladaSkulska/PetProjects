using HDrezka.Models.DTOs;

namespace HDrezka.Services.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetMoviesAsync();
        Task<MovieDetailsDto> GetMovieByIdAsync(int id);
        Task<MovieDto> AddMovieAsync(MovieDetailsDto movieDto);
        Task UpdateMovieAsync(int id, MovieDetailsDto movieDto);
        Task DeleteMovieAsync(int id);
        Task<IEnumerable<MovieDto>> FilterMoviesAsync(string genre, string title, string type);
    }
}