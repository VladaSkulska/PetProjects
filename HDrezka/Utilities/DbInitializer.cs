using HDrezka.EF;
using HDrezka.Models;

namespace HDrezka.Utilities
{
    public class DbInitializer
    {
        public static void SeedMovies(AppDbContext context)
        {
            if (context.Movies.Any())
            {
                return;
            }

            var movies = new List<Movie>
            {
                new Movie
                {
                    Title = "The Shawshank Redemption",
                    Genre = MovieGenre.Drama,
                    MovieType = MovieType.Movie,
                    Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                    DurationMinutes = 142,
                    Director = "Frank Darabont",
                    ReleaseDate = DateTime.SpecifyKind(new DateTime(1994, 10, 14), DateTimeKind.Utc)
                },
                new Movie
                {
                    Title = "The Godfather",
                    Genre = MovieGenre.Drama,
                    MovieType = MovieType.Movie,
                    Description = "An organized crime dynasty's aging patriarch transfers control of his clandestine empire to his reluctant son.",
                    DurationMinutes = 175,
                    Director = "Francis Ford Coppola",
                    ReleaseDate = DateTime.SpecifyKind(new DateTime(1972, 3, 24), DateTimeKind.Utc)
                },
                new Movie
                {
                    Title = "The Godfather: Part II",
                    Genre = MovieGenre.Crime,
                    MovieType = MovieType.Movie,
                    Description = "The early life and career of Vito Corleone in 1920s New York City is portrayed, while his son, Michael, expands and tightens his grip on the family crime syndicate.",
                    DurationMinutes = 202,
                    Director = "Francis Ford Coppola",
                    ReleaseDate = DateTime.SpecifyKind(new DateTime(1974, 12, 20), DateTimeKind.Utc)
                },
                new Movie
                {
                    Title = "Forrest Gump",
                    Genre = MovieGenre.Drama,
                    MovieType = MovieType.Movie,
                    Description = "The presidencies of Kennedy and Johnson, the Vietnam War, the Watergate scandal and other historical events unfold from the perspective of an Alabama man with an IQ of 75, whose only desire is to be reunited with his childhood sweetheart.",
                    DurationMinutes = 142,
                    Director = "Robert Zemeckis",
                    ReleaseDate = DateTime.SpecifyKind(new DateTime(1994, 7, 6), DateTimeKind.Utc)
                },
                new Movie
                {
                    Title = "The Lord of the Rings: The Return of the King",
                    Genre = MovieGenre.Fantasy,
                    MovieType = MovieType.Movie,
                    Description = "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.",
                    DurationMinutes = 201,
                    Director = "Peter Jackson",
                    ReleaseDate = DateTime.SpecifyKind(new DateTime(2003, 12, 17), DateTimeKind.Utc)
                },
                new Movie
                {
                    Title = "Fight Club",
                    Genre = MovieGenre.Drama,
                MovieType = MovieType.Movie,
                    Description = "An insomniac office worker and a devil-may-care soapmaker form an underground fight club that evolves into something much, much more.",
                    DurationMinutes = 139,
                    Director = "David Fincher",
                    ReleaseDate = DateTime.SpecifyKind(new DateTime(1999, 10, 15), DateTimeKind.Utc)
                },
                new Movie
                {
                    Title = "Inception",
                    Genre = MovieGenre.Action,
                    MovieType = MovieType.Movie,
                    Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.",
                    DurationMinutes = 148,
                    Director = "Christopher Nolan",
                    ReleaseDate = DateTime.SpecifyKind(new DateTime(2010, 7, 16), DateTimeKind.Utc)
                },
                new Movie
                {
                Title = "The Matrix",
                    Genre = MovieGenre.Action,
                    MovieType = MovieType.Movie,
                    Description = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
                    DurationMinutes = 136,
                    Director = "Lana Wachowski, Lilly Wachowski",
                    ReleaseDate = DateTime.SpecifyKind(new DateTime(1999, 3, 31), DateTimeKind.Utc)
                }
            };

            context.Movies.AddRange(movies);

            context.SaveChanges();
        }
    }
}
