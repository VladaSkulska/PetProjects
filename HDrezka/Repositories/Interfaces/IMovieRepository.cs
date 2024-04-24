using HDrezka.Models;

namespace HDrezka.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetMoviesAsync();
        Task<Movie> GetMovieByIdAsync(int id);
        Task AddMovieAsync(Movie movie);
        Task<Movie> DeleteMovieAsync(int id);
        Task<int> SaveAsync();
    }
}
