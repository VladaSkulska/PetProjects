using HDrezka.Models;
using HDrezka.Utilities;

namespace HDrezka.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly JsonFileManager _jsonFileManager = new();

        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            return await _jsonFileManager.LoadFromJsonFileAsync<List<Movie>>();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movies = await GetMoviesAsync();
            return movies.FirstOrDefault(movie => movie.Id == id);
        }

        public async Task AddMovieAsync(Movie movie)
        {
            var movies = new List<Movie>(await GetMoviesAsync());
            movies.Add(movie);
            await _jsonFileManager.SaveToJsonFileAsync(movies);
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            var movies = new List<Movie>(await GetMoviesAsync());
            int index = movies.FindIndex(movie => movie.Id == movie.Id);
            if (index != -1)
            {
                movies[index] = movie;
                await _jsonFileManager.SaveToJsonFileAsync(movies);
            }
        }

        public async Task<Movie> DeleteMovieAsync(int id)
        {
            var movies = new List<Movie>(await GetMoviesAsync());
            var movieToRemove = movies.FirstOrDefault(movie => movie.Id == id);
            if (movieToRemove != null)
            {
                movies.Remove(movieToRemove);
                await _jsonFileManager.SaveToJsonFileAsync(movies);
            }
            return movieToRemove;
        }
    }
}