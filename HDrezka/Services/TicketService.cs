using AutoMapper;
using HDrezka.Models;
using HDrezka.Models.DTOs;
using HDrezka.Repositories;
using HDrezka.Repositories.Interfaces;
using HDrezka.Services.Interfaces;

namespace HDrezka.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMovieScheduleRepository _movieScheduleRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly IMapper _mapper;

        public TicketService(ITicketRepository ticketRepository, IMovieScheduleRepository movieScheduleRepository,
            ISeatRepository seatRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _movieScheduleRepository = movieScheduleRepository;
            _seatRepository = seatRepository;
            _mapper = mapper;
        }

        public async Task<TicketDto> BuyTicketAsync(int movieScheduleId, int seatNumber)
        {
            var seat = await _seatRepository.GetSeatByNumberAsync(seatNumber);
            if (seat == null || !seat.IsAvailable)
                throw new InvalidOperationException("Seat is not available");

            await _seatRepository.MarkSeatAsUnavailableAsync(seat);

            var purchaseTime = DateTime.UtcNow;

            var movieSchedule = await _movieScheduleRepository.GetMovieScheduleByIdAsync(movieScheduleId);
            if (movieSchedule == null)
                throw new ArgumentException("Movie schedule not found");

            var expirationTime = movieSchedule.ShowTime.Add(TimeSpan.FromMinutes(movieSchedule.Movie.DurationMinutes));

            var ticket = new Ticket
            {
                MovieScheduleId = movieScheduleId,
                MovieScheduleInst = movieSchedule,
                SeatId = seat.Id,
                Seat = seat,
                PurchaseTime = purchaseTime,
                ExpirationTime = expirationTime
            };

            await _ticketRepository.AddTicketAsync(ticket);
            await _ticketRepository.SaveAsync();

            var ticketDto = _mapper.Map<TicketDto>(ticket);
            return ticketDto;
        }
    }
}