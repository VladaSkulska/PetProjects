using HDrezka.Controllers;
using HDrezka.Models;
using Microsoft.AspNetCore.Mvc;

namespace HDrezka.Middlewares
{
    public class MovieMiddleware
    {
        public MovieMiddleware()
        {

        }

        public async Task InvokeAsync(HttpContext context, MoviesController moviesController)
        {
            if (context.Request.Path.StartsWithSegments("/api/movies", StringComparison.OrdinalIgnoreCase))
            {
                switch (context.Request.Method.ToUpperInvariant())
                {
                    case "GET":
                        await GetMoviesAsync(context, moviesController);
                        break;
                    case "POST":
                        await AddMovieAsync(context, moviesController);
                        break;
                    case "GETFILTERED":
                        await FilterMoviesAsync(context, moviesController);
                        break;
                    case "GETDESCRIPTION":
                        await GetMovieDescriptionAsync(context, moviesController);
                        break;
                    default:
                        context.Response.StatusCode = 405;
                        break;
                }
            }
        }

        private async Task GetMoviesAsync(HttpContext context, MoviesController moviesController)
        {
            var result = await moviesController.GetMovies();
            await WriteResponseAsync(context, result);
        }

        private async Task AddMovieAsync(HttpContext context, MoviesController moviesController)
        {
            var movie = await context.Request.ReadFromJsonAsync<Movie>();
            var result = await moviesController.AddMovie(movie);
            await WriteResponseAsync(context, result);
        }

        private async Task FilterMoviesAsync(HttpContext context, MoviesController moviesController)
        {
            var genre = context.Request.Query["genre"];
            var name = context.Request.Query["name"];
            var type = context.Request.Query["type"];
            var result = await moviesController.FilterMovies(genre, name, type);
            await WriteResponseAsync(context, result);
        }

        private async Task GetMovieDescriptionAsync(HttpContext context, MoviesController moviesController)
        {
            if (!int.TryParse(context.Request.Query["id"], out int id))
            {
                context.Response.StatusCode = 400;
                return;
            }

            var result = await moviesController.GetMovieDescription(id);
            await WriteResponseAsync(context, result);
        }

        private async Task WriteResponseAsync(HttpContext context, IActionResult result)
        {
            var objectResult = result as ObjectResult;
            if (objectResult != null)
            {
                context.Response.StatusCode = objectResult.StatusCode ?? 200;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(objectResult.Value);
            }
            else
            {
                context.Response.StatusCode = 500;
            }
        }

    }
}
