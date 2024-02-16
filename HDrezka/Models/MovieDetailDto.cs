namespace HDrezka.Models
{
    public class MovieDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string MovieType { get; set; }
        public string Description { get; set; }
        public int DurationMinutes { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}