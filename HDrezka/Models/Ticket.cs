using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HDrezka.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("MovieScheduleId")]
        public int MovieScheduleId { get; set; }
        public MovieSchedule MovieSchedule { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [Required]
        public int SeatNumber { get; set; }
        public DateTime PurchaseTime { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}