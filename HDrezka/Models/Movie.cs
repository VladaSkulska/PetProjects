using System.ComponentModel.DataAnnotations;

namespace HDrezka.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public MovieGenre Genre { get; set; }
        public MovieType MovieType { get; set; }
        public string Description { get; set; }
        public int DurationMinutes { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}