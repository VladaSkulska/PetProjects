using HDrezka.Data;
using HDrezka.Models;
using HDrezka.Repositories.Interfaces;

namespace HDrezka.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly AppDbContext _dbContext;

        public SeatRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Seat> GetSeatByNumberAsync(int seatNumber)
        {
            throw new NotImplementedException();
        }

        public async Task MarkSeatAsUnavailableAsync(Seat seat)
        {
            throw new NotImplementedException();
        }
    }
}