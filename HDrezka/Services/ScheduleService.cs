using HDrezka.Models;
using HDrezka.Models.DTOs;
using HDrezka.Repositories;

namespace HDrezka.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleService(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task<IEnumerable<ScheduleDto>> GetSchedulesAsync()
        {
            var schedules = await _scheduleRepository.GetSchedulesAsync();
            return schedules.Select(MapToDto);
        }

        public async Task<ScheduleDto> GetScheduleByIdAsync(int id)
        {
            var schedule = await _scheduleRepository.GetScheduleByIdAsync(id);
            if (schedule == null)
            {
                return null;
            }

            return MapToDto(schedule);
        }

        public async Task<ScheduleDto> GetScheduleForDateAsync(DateTime date)
        {
            var schedule = await _scheduleRepository.GetScheduleForDateAsync(date);
            return MapToDto(schedule);
        }

        public async Task<ScheduleDto> AddScheduleAsync(ScheduleDto scheduleDto)
        {
            if (scheduleDto == null)
            {
                throw new ArgumentNullException(nameof(scheduleDto), "Schedule DTO cannot be null.");
            }

            var schedule = MapToEntity(scheduleDto);
            await _scheduleRepository.AddScheduleAsync(schedule);
            await _scheduleRepository.SaveAsync();

            return MapToDto(schedule);
        }

        public async Task UpdateScheduleAsync(int id, ScheduleDto scheduleDto)
        {
            var existingSchedule = await _scheduleRepository.GetScheduleByIdAsync(id);
            if (existingSchedule == null)
            {
                throw new KeyNotFoundException($"Schedule with ID {id} not found");
            }

            var schedule = MapToEntity(scheduleDto);
            MapToEntity(schedule, existingSchedule);
            await _scheduleRepository.SaveAsync();
        }

        public async Task DeleteScheduleAsync(int id)
        {
            var existingSchedule = await _scheduleRepository.GetScheduleByIdAsync(id);
            if (existingSchedule == null)
            {
                throw new KeyNotFoundException($"Schedule with ID {id} not found");
            }

            await _scheduleRepository.DeleteScheduleAsync(id);
            await _scheduleRepository.SaveAsync();
        }

        private ScheduleDto MapToDto(Schedule schedule)
        {
            return new ScheduleDto
            {
                Id = schedule.Id,
                Date = schedule.Date,
                MovieSchedules = schedule.MovieSchedules.Select(ms => new MovieScheduleDto
                {
                    Id = ms.Id,
                    MovieId = ms.MovieId,
                    Order = ms.Order,
                    ShowTime = ms.ShowTime,
                    Seats = ms.Seats.Select(s => new SeatDto
                    {
                        Id = s.Id,
                        SeatNumber = s.SeatNumber,
                        IsAvailable = s.IsAvailable
                    }).ToList(),
                    Tickets = ms.Tickets.Select(t => new TicketDto
                    {
                        Id = t.Id,
                        UserId = t.UserId,
                        SeatNumber = t.SeatNumber,
                        PurchaseTime = t.PurchaseTime,
                        ExpirationTime = t.ExpirationTime
                    }).ToList(),
                }).ToList()
            };
        }

        private Schedule MapToEntity(ScheduleDto scheduleDto)
        {
            var schedule = new Schedule
            {
                Id = scheduleDto.Id,
                Date = scheduleDto.Date
            };

            if (scheduleDto.MovieSchedules != null)
            {
                schedule.MovieSchedules = new List<MovieSchedule>();

                foreach (var movieScheduleDto in scheduleDto.MovieSchedules)
                {
                    var movieSchedule = new MovieSchedule
                    {
                        Id = movieScheduleDto.Id,
                        MovieId = movieScheduleDto.MovieId,
                        Order = movieScheduleDto.Order,
                        ShowTime = movieScheduleDto.ShowTime
                    };

                    if (movieScheduleDto.Seats != null)
                    {
                        movieSchedule.Seats = movieScheduleDto.Seats.Select(seatDto => new Seat
                        {
                            Id = seatDto.Id,
                            SeatNumber = seatDto.SeatNumber,
                            IsAvailable = seatDto.IsAvailable
                        }).ToList();
                    }

                    if (movieScheduleDto.Tickets != null)
                    {
                        movieSchedule.Tickets = movieScheduleDto.Tickets.Select(ticketDto => new Ticket
                        {
                            Id = ticketDto.Id,
                            UserId = ticketDto.UserId,
                            SeatNumber = ticketDto.SeatNumber,
                            PurchaseTime = ticketDto.PurchaseTime,
                            ExpirationTime = ticketDto.ExpirationTime
                        }).ToList();
                    }

                    schedule.MovieSchedules.Add(movieSchedule);
                }
            }

            return schedule;
        }

        private void MapToEntity(Schedule schedule, Schedule existingSchedule)
        {
            existingSchedule.Date = schedule.Date;

            existingSchedule.MovieSchedules.Clear();

            if (schedule.MovieSchedules != null)
            {
                foreach (var movieScheduleDto in schedule.MovieSchedules)
                {
                    var movieSchedule = new MovieSchedule
                    {
                        Id = movieScheduleDto.Id,
                        MovieId = movieScheduleDto.MovieId,
                        Order = movieScheduleDto.Order,
                        ShowTime = movieScheduleDto.ShowTime
                    };

                    if (movieScheduleDto.Seats != null)
                    {
                        movieSchedule.Seats = movieScheduleDto.Seats.Select(seatDto => new Seat
                        {
                            Id = seatDto.Id,
                            SeatNumber = seatDto.SeatNumber,
                            IsAvailable = seatDto.IsAvailable
                        }).ToList();
                    }

                    if (movieScheduleDto.Tickets != null)
                    {
                        movieSchedule.Tickets = movieScheduleDto.Tickets.Select(ticketDto => new Ticket
                        {
                            Id = ticketDto.Id,
                            UserId = ticketDto.UserId,
                            SeatNumber = ticketDto.SeatNumber,
                            PurchaseTime = ticketDto.PurchaseTime,
                            ExpirationTime = ticketDto.ExpirationTime
                        }).ToList();
                    }

                    existingSchedule.MovieSchedules.Add(movieSchedule);
                }
            }
        }
    }
}
