using HDrezka.Data;
using HDrezka.Repositories.Interfaces;

namespace HDrezka.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _dbContext;
        public TicketRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task RemoveTicketAsync(int ticketId)
        {
            var ticket = await _dbContext.Tickets.FindAsync(ticketId);
            if (ticket != null)
            {
                _dbContext.Tickets.Remove(ticket);
            }
            else
            {
                throw new InvalidOperationException("Ticket not found.");
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}