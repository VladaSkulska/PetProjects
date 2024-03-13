using HDrezka.Models.DTOs;

namespace HDrezka.Services.Interfaces
{
    public interface ITicketService
    {
        Task<TicketDto> BuyTicketAsync(int movieScheduleId, int seatNumber);
    }
}