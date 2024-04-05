using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using HDrezka.Models.DTOs;
using HDrezka.Models;
using HDrezka.Repositories.Interfaces;
using HDrezka.Services;
using HDrezka.Services.Interfaces;

namespace HDrezka.Tests.Services
{
    public class ScheduleServiceTests
    {
        private IScheduleRepository _scheduleRepository;
        private IMapper _mapper;
        public ScheduleServiceTests()
        {
            _scheduleRepository = A.Fake<IScheduleRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async Task GetSchedulesAsync_WhenSchedulesExist_ReturnsMappedSchedulesDto()
        {
            // Arrange
            var scheduleService = new ScheduleService(_scheduleRepository, _mapper);
            var schedules = new List<Schedule>
            {
                new Schedule { Id = 1, Date = DateTime.Now, MovieSchedules = new List<MovieSchedule>() },
                new Schedule { Id = 2, Date = DateTime.Now.AddDays(1), MovieSchedules = new List<MovieSchedule>() },
                new Schedule { Id = 3, Date = DateTime.Now.AddDays(2), MovieSchedules = new List<MovieSchedule>() }
            };
            A.CallTo(() => _scheduleRepository.GetSchedulesAsync()).Returns(Task.FromResult<IEnumerable<Schedule>>(schedules));

            var expectedSchedules = schedules.Select(s => new ScheduleDto
            {
                Id = s.Id,
                Date = s.Date,
            });
            A.CallTo(() => _mapper.Map<IEnumerable<ScheduleDto>>(schedules)).Returns(expectedSchedules);

            // Act
            var result = await scheduleService.GetSchedulesAsync();

            // Assert
            result.Should().BeEquivalentTo(expectedSchedules);
        }

        [Fact]
        public async Task GetScheduleByIdAsync_ExistingId_ReturnsMappedScheduleDto()
        {
            // Arrange
            var id = 1;
            var schedule = new Schedule
            {
                Id = id,
                Date = DateTime.Now,
                MovieSchedules = new List<MovieSchedule>()
            };
            var scheduleService = new ScheduleService(_scheduleRepository, _mapper);
            A.CallTo(() => _scheduleRepository.GetScheduleByIdAsync(id)).Returns(Task.FromResult(schedule));

            var expectedScheduleDto = new ScheduleDto
            {
                Id = schedule.Id,
                Date = schedule.Date
            };
            A.CallTo(() => _mapper.Map<ScheduleDto>(schedule)).Returns(expectedScheduleDto);

            // Act
            var result = await scheduleService.GetScheduleByIdAsync(id);

            // Assert
            result.Should().BeEquivalentTo(expectedScheduleDto);
        }

        [Fact]
        public async Task GetScheduleForDateAsync_ExistingDate_ReturnsMappedScheduleDto()
        {
            // Arrange
            var date = DateTime.Now;
            var schedule = new Schedule
            {
                Id = 1,
                Date = date,
                MovieSchedules = new List<MovieSchedule>()
            };

            var scheduleService = new ScheduleService(_scheduleRepository, _mapper);
            A.CallTo(() => _scheduleRepository.GetScheduleForDateAsync(date)).Returns(Task.FromResult(schedule));

            var expectedScheduleDto = new ScheduleDto
            {
                Id = schedule.Id,
                Date = schedule.Date
            };

            A.CallTo(() => _mapper.Map<ScheduleDto>(schedule)).Returns(expectedScheduleDto);

            // Act
            var result = await scheduleService.GetScheduleForDateAsync(date);

            // Assert
            result.Should().BeEquivalentTo(expectedScheduleDto);
        }

        [Fact]
        public async Task AddScheduleAsync_ValidScheduleDto_ReturnsMappedScheduleDto()
        {
            // Arrange
            var scheduleDto = new ScheduleDto
            {
                Date = DateTime.Now,
                MovieSchedules = new List<MovieScheduleDto>
                {
                    new MovieScheduleDto { MovieId = 1}
                }
            };

            var schedule = new Schedule
            {
                Id = 1,
                Date = DateTime.Now,
                MovieSchedules = new List<MovieSchedule>
                {
                    new MovieSchedule { MovieId = 1}
                }
            };

            var scheduleService = new ScheduleService(_scheduleRepository, _mapper);

            A.CallTo(() => _mapper.Map<Schedule>(scheduleDto)).Returns(schedule);

            var expectedScheduleDto = new ScheduleDto
            {
                Date = DateTime.Now,
                MovieSchedules = new List<MovieScheduleDto>
                {
                    new MovieScheduleDto { MovieId = 1}
                }
            };
            A.CallTo(() => _mapper.Map<ScheduleDto>(schedule)).Returns(expectedScheduleDto);

            // Act
            var result = await scheduleService.AddScheduleAsync(scheduleDto);

            // Assert
            result.Should().BeEquivalentTo(expectedScheduleDto);
            A.CallTo(() => _scheduleRepository.SaveAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateScheduleAsync_ExistingId_ReturnsNoException()
        {
            // Arrange
            var id = 1;
            var scheduleDto = new ScheduleDto();
            var scheduleService = new ScheduleService(_scheduleRepository, _mapper);
            var existingSchedule = new Schedule
            {
                Id = id,
                Date = DateTime.Now,
                MovieSchedules = new List<MovieSchedule>()
                {
                    new MovieSchedule()
                }
            };

            A.CallTo(() => _scheduleRepository.GetScheduleByIdAsync(id)).Returns(Task.FromResult(existingSchedule));

            // Act & Assert
            await FluentActions.Invoking(async () => await scheduleService.UpdateScheduleAsync(id, scheduleDto))
                .Should().NotThrowAsync<KeyNotFoundException>();

            A.CallTo(() => _scheduleRepository.SaveAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task DeleteScheduleAsync_ExistingId_ReturnsNoException()
        {
            // Arrange
            var id = 1;
            var scheduleService = new ScheduleService(_scheduleRepository, _mapper);
            var existingSchedule = new Schedule
            {
                Id = id,
                Date = DateTime.Now,
                MovieSchedules = new List<MovieSchedule>()
            };

            A.CallTo(() => _scheduleRepository.GetScheduleByIdAsync(id)).Returns(Task.FromResult(existingSchedule));

            // Act & Assert
            await FluentActions.Invoking(async () => await scheduleService.DeleteScheduleAsync(id))
                .Should().NotThrowAsync<KeyNotFoundException>();

            A.CallTo(() => _scheduleRepository.SaveAsync()).MustHaveHappenedOnceExactly();
        }
    }
}
