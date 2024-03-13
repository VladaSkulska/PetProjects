using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HDrezka.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(MovieSchedule))]
        public int MovieScheduleId { get; set; }
        public MovieSchedule MovieScheduleInst { get; set; }

        [ForeignKey(nameof(Seat))]
        public int SeatId { get; set; }
        public Seat Seat { get; set; }
        public DateTime? PurchaseTime { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}