using HDrezka.Models;

namespace HDrezka.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task AddTicketAsync(Ticket ticket);
        Task RemoveTicketAsync(int ticketId);
        Task<Ticket> GetTicketByIdAsync(int ticketId);
        Task<int> SaveAsync();
    }
}