using AutoMapper;
using HDrezka.Models;
using HDrezka.Models.DTOs;
using HDrezka.Repositories.Interfaces;
using HDrezka.Services.Interfaces;

namespace HDrezka.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IMapper _scheduleMapper;
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleService(IScheduleRepository scheduleRepository, IMapper scheduleMapper)
        {
            _scheduleRepository = scheduleRepository;
            _scheduleMapper = scheduleMapper;
        }

        public async Task<IEnumerable<ScheduleDto>> GetSchedulesAsync()
        {
            var schedules = await _scheduleRepository.GetSchedulesAsync();
            return _scheduleMapper.Map<IEnumerable<ScheduleDto>>(schedules);
        }

        public async Task<ScheduleDto> GetScheduleByIdAsync(int id)
        {
            var schedule = await _scheduleRepository.GetScheduleByIdAsync(id);
            if (schedule == null)
            {
                return null;
            }

            return _scheduleMapper.Map<ScheduleDto>(schedule);
        }

        public async Task<ScheduleDto> GetScheduleForDateAsync(DateTime date)
        {
            var schedule = await _scheduleRepository.GetScheduleForDateAsync(date);
            return _scheduleMapper.Map<ScheduleDto>(schedule);
        }

        public async Task<ScheduleDto> AddScheduleAsync(ScheduleDto scheduleDto)
        {
            if (scheduleDto == null)
            {
                throw new ArgumentNullException(nameof(scheduleDto), "Schedule DTO cannot be null.");
            }

            var schedule = _scheduleMapper.Map<Schedule>(scheduleDto);
            await _scheduleRepository.AddScheduleAsync(schedule);
            await _scheduleRepository.SaveAsync();

            return _scheduleMapper.Map<ScheduleDto>(schedule);
        }

        public async Task UpdateScheduleAsync(int id, ScheduleDto scheduleDto)
        {
            var existingSchedule = await _scheduleRepository.GetScheduleByIdAsync(id);
            if (existingSchedule == null)
            {
                throw new KeyNotFoundException($"Schedule with ID {id} not found");
            }

            _scheduleMapper.Map(scheduleDto, existingSchedule);
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
    }
}