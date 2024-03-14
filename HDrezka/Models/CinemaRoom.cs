using System.ComponentModel.DataAnnotations;

namespace HDrezka.Models
{
    public class CinemaRoom
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxSeats { get; set; }
        public ICollection<Seat> Seats { get; set; }
        public ICollection<Ticket> Tickets { get; set; }

        public CinemaRoom()
        {
            Seats = new List<Seat>();
            Tickets = new List<Ticket>();
        }

        public void InitializeSeatsAndTickets()
        {
            for (int i = 1; i <= MaxSeats; i++)
            {
                var seat = new Seat { SeatNumber = i, IsAvailable = true };
                var ticket = new Ticket { SeatId = i, PurchaseTime = null, ExpirationTime = null };

                Seats.Add(seat);
                Tickets.Add(ticket);
            }
        }
    }
}