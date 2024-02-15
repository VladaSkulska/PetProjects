namespace HDrezka.Models
{
    public class Movie
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public MovieType MovieType { get; set; }
        public MovieGenre Genre { get; set; }
        public int DurationMinutes { get; set; }
        public string Director { get; set; }

    }
}
