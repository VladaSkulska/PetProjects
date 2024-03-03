using HDrezka.Models.DTOs;

namespace HDrezka.Services
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleDto>> GetSchedulesAsync();
        Task<ScheduleDto> GetScheduleByIdAsync(int id);
        Task<ScheduleDto> GetScheduleForDateAsync(DateTime date);
        Task<ScheduleDto> AddScheduleAsync(ScheduleDto scheduleDto);
        Task UpdateScheduleAsync(int id, ScheduleDto scheduleDto);
        Task DeleteScheduleAsync(int id);
    }
}