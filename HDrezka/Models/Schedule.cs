using System.ComponentModel.DataAnnotations;

namespace HDrezka.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; } 

        public ICollection<MovieSchedule> MovieSchedules { get; set; }
    }
}