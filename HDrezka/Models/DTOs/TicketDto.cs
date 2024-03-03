namespace HDrezka.Models.DTOs
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int SeatNumber { get; set; }
        public DateTime PurchaseTime { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}