using HDrezka.Data;
using HDrezka.Models;
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

        public async Task AddTicketAsync(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveTicketAsync(int ticketId)
        {
            throw new NotImplementedException();
        }

        public async Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}