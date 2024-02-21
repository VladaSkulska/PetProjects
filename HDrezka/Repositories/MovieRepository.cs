using HDrezka.EF;
using HDrezka.Models;
using Microsoft.EntityFrameworkCore;

namespace HDrezka.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _dbContext;

        public MovieRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            return await _dbContext.Movies.ToListAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _dbContext.Movies.FindAsync(id);
        }

        public async Task AddMovieAsync(Movie movie)
        {
            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            _dbContext.Entry(movie).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Movie> DeleteMovieAsync(int id)
        {
            var movieToRemove = await _dbContext.Movies.FindAsync(id);
            if (movieToRemove != null)
            {
                _dbContext.Movies.Remove(movieToRemove);
                await _dbContext.SaveChangesAsync();
            }
            return movieToRemove;
        }
    }
}