namespace HDrezka.Models.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public MovieGenre Genre { get; set; }
        public MovieType MovieType { get; set; }
        public string Description { get; set; }
    }
}