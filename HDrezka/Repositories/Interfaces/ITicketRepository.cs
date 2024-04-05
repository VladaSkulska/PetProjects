namespace HDrezka.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task RemoveTicketAsync(int ticketId);
        Task<int> SaveAsync();
    }
}