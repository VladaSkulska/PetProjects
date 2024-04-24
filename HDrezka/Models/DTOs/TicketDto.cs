namespace HDrezka.Models.DTOs
{
    public class TicketDto
    {
        public int Id { get; set; }
        public int MovieScheduleId { get; set; }
        public int SeatNumber { get; set; }
        public DateTime PurchaseTime { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}