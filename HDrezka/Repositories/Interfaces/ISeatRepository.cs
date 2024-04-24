using HDrezka.Models;
using Microsoft.EntityFrameworkCore;

namespace HDrezka.Repositories.Interfaces
{
    public interface ISeatRepository
    {
        Task MarkSeatAsUnavailableAsync(Seat seat);
        Task<int> SaveAsync();
    }
}
