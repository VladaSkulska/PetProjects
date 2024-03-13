using HDrezka.Models;

namespace HDrezka.Repositories.Interfaces
{
    public interface IScheduleRepository
    {
        Task<IEnumerable<Schedule>> GetSchedulesAsync();
        Task<IEnumerable<MovieSchedule>> GetMoviesFromScheduleAsync(int scheduleId);
        Task<Schedule> GetScheduleForDateAsync(DateTime date);
        Task<Schedule> GetScheduleByIdAsync(int id);
        Task<Schedule> AddScheduleAsync(Schedule schedule);
        Task<Schedule> DeleteScheduleAsync(int scheduleId);
        Task<int> SaveAsync();
    }
}