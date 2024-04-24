using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using HDrezka.Models;
using HDrezka.Models.DTOs;
using HDrezka.Repositories;
using HDrezka.Repositories.Interfaces;
using HDrezka.Services;
using HDrezka.Utilities.Exceptions;
using System.Net;
namespace HDrezka.Tests.Services
{
    public class TicketServiceTests
    {
        private ITicketRepository _ticketRepository;
        private IMovieScheduleRepository _movieScheduleRepository;
        private ISeatRepository _seatRepository;
        private IMapper _mapper;

        public TicketServiceTests()
        {
            _ticketRepository = A.Fake<ITicketRepository>();
            _movieScheduleRepository = A.Fake<IMovieScheduleRepository>();
            _seatRepository = A.Fake<ISeatRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async Task BuyTicketAsync_WhenSuccessfulPurchase_ReturnsTicketDto()
        {
            // Arrange
            var ticketService = new TicketService(_ticketRepository, _movieScheduleRepository, _seatRepository, _mapper);
            var movieScheduleId = 1;
            var seatNumber = 5;
            var movieSchedule = new MovieSchedule
            {
                Id = movieScheduleId,
                ShowTime = DateTime.UtcNow.AddHours(1), // Example: 1 hour in future
                Movie = new Movie { DurationMinutes = 120 } // Example: Movie duration 120 minutes
            };
            var cinemaRoom = new CinemaRoom
            {
                Seats = new List<Seat>
                {
                    new Seat { SeatNumber = seatNumber, IsAvailable = true }
                }
            };
            movieSchedule.CinemaRoom = cinemaRoom;

            A.CallTo(() => _movieScheduleRepository.GetMovieScheduleByIdAsync(movieScheduleId)).Returns(movieSchedule);
            A.CallTo(() => _seatRepository.MarkSeatAsUnavailableAsync(A<Seat>._)).Returns(Task.CompletedTask);

            // Act
            var result = await ticketService.BuyTicketAsync(movieScheduleId, seatNumber);

            // Assert
            result.Should().BeAssignableTo<TicketDto>();
        }

        [Fact]
        public async Task BuyTicketAsync_WhenMovieScheduleNotFound_ThrowsException()
        {
            // Arrange
            var ticketService = new TicketService(_ticketRepository, _movieScheduleRepository, _seatRepository, _mapper);
            A.CallTo(() => _movieScheduleRepository.GetMovieScheduleByIdAsync(A<int>._)).Returns(Task.FromResult<MovieSchedule>(null));

            // Act
            Func<Task> act = async () => await ticketService.BuyTicketAsync(1, 5);

            // Assert
            var exceptionAssertion = await act.Should().ThrowAsync<TicketOperationException>().WithMessage("Movie schedule with ID 1 not found.");
            exceptionAssertion.Which.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task BuyTicketAsync_WhenCinemaRoomNotFound_ThrowsException()
        {
            // Arrange
            var ticketService = new TicketService(_ticketRepository, _movieScheduleRepository, _seatRepository, _mapper);
            var movieSchedule = new MovieSchedule { CinemaRoom = null };
            A.CallTo(() => _movieScheduleRepository.GetMovieScheduleByIdAsync(A<int>._)).Returns(movieSchedule);

            // Act
            Func<Task> act = async () => await ticketService.BuyTicketAsync(1, 5);

            // Assert
            var exceptionAssertion = await act.Should().ThrowAsync<TicketOperationException>().WithMessage("Cinema room not found.");
            exceptionAssertion.Which.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task BuyTicketAsync_WhenSeatNotAvailable_ThrowsException()
        {
            // Arrange
            var seatNumber = 5;
            var ticketService = new TicketService(_ticketRepository, _movieScheduleRepository, _seatRepository, _mapper);
            var cinemaRoom = new CinemaRoom
            {
                Seats = new List<Seat>
                {
                    new Seat { SeatNumber = seatNumber, IsAvailable = false }
                }
            };
            var movieSchedule = new MovieSchedule { CinemaRoom = cinemaRoom };
            A.CallTo(() => _movieScheduleRepository.GetMovieScheduleByIdAsync(A<int>._)).Returns(movieSchedule);

            // Act
            Func<Task> act = async () => await ticketService.BuyTicketAsync(1, 5);

            // Assert
            var exceptionAssertion = await act.Should().ThrowAsync<TicketOperationException>().WithMessage("Seat is not available.");
            exceptionAssertion.Which.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
