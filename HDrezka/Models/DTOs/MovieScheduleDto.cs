namespace HDrezka.Models.DTOs
{
    public class MovieScheduleDto
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int Order { get; set; }
        public DateTime ShowTime { get; set; }
        public ICollection<SeatDto> Seats { get; set; }
        public ICollection<TicketDto> Tickets { get; set; }
    }
}