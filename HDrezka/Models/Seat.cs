using System.ComponentModel.DataAnnotations;

namespace HDrezka.Models
{
    public class Seat
    {
        [Key]
        public int Id { get; set; }
        public int SeatNumber { get; set; }
        public bool IsAvailable { get; set; }
    }
}