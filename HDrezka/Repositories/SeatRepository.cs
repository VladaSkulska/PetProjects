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

        public async Task MarkSeatAsUnavailableAsync(Seat seat)
        {
            if (seat == null)
            {
                throw new ArgumentNullException(nameof(seat), "Seat cannot be null.");
            }

            seat.IsAvailable = false;
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}