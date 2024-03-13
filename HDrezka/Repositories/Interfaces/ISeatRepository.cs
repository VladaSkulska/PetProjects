using HDrezka.Models;

namespace HDrezka.Repositories.Interfaces
{
    public interface ISeatRepository
    {
        Task<Seat> GetSeatByNumberAsync(int seatNumber);
        Task MarkSeatAsUnavailableAsync(Seat seat);
    }
}
