namespace HDrezka.Models.DTOs
{
    public class ScheduleDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ICollection<MovieScheduleDto> MovieSchedules { get; set; }
    }
}