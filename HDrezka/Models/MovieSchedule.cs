using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HDrezka.Models
{
    public class MovieSchedule
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey(nameof(Schedule))]
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
        public int Order { get; set; } 
        public DateTime ShowTime { get; set; }
        public ICollection<Seat> Seats { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}