using AutoMapper;
using HDrezka.Models;
using HDrezka.Models.DTOs;
using HDrezka.Repositories;
using HDrezka.Repositories.Interfaces;
using HDrezka.Services.Interfaces;
using HDrezka.Utilities.Exceptions;
using System.Net;

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
            var movieSchedule = await _movieScheduleRepository.GetMovieScheduleByIdAsync(movieScheduleId);
            if (movieSchedule == null)
            {
                throw new TicketOperationException($"Movie schedule with ID {movieScheduleId} not found.", HttpStatusCode.NotFound);
            }

            var cinemaRoom = movieSchedule.CinemaRoom;
            if (cinemaRoom == null)
            {
                throw new TicketOperationException("Cinema room not found.", HttpStatusCode.NotFound);
            }

            var seat = cinemaRoom.Seats.FirstOrDefault(s => s.SeatNumber == seatNumber);
            if (seat == null || !seat.IsAvailable)
            {
                throw new TicketOperationException("Seat is not available.", HttpStatusCode.BadRequest);
            }

            await _seatRepository.MarkSeatAsUnavailableAsync(seat);

            var purchaseTime = DateTime.UtcNow;
            var expirationTime = movieSchedule.ShowTime.Add(TimeSpan.FromMinutes(movieSchedule.Movie.DurationMinutes));

            var ticket = new Ticket
            {
                MovieScheduleId = movieScheduleId,
                SeatId = seatNumber,
                PurchaseTime = purchaseTime,
                ExpirationTime = expirationTime
            };

            await _ticketRepository.RemoveTicketAsync(ticket.Id);
            await _ticketRepository.SaveAsync();

            var ticketDto = _mapper.Map<TicketDto>(ticket);
            return ticketDto;
        }
    }
}