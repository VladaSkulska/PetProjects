using HDrezka.Data;
using HDrezka.Models;
using Microsoft.EntityFrameworkCore;

namespace HDrezka.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly AppDbContext _dbContext;

        public ScheduleRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesAsync()
        {
            return await _dbContext.Schedules.ToListAsync();
        }

        public async Task<Schedule> GetScheduleForDateAsync(DateTime date)
        {
            return await _dbContext.Schedules
                .FirstOrDefaultAsync(s => s.Date.Date == date);
        }

        public async Task<IEnumerable<MovieSchedule>> GetMoviesFromScheduleAsync(int scheduleId)
        {
            return await _dbContext.Schedules
                .Where(s => s.Id == scheduleId)
                .SelectMany(s => s.MovieSchedules)
                .ToListAsync();
        }

        public async Task<Schedule> GetScheduleByIdAsync(int scheduleId)
        {
            return await _dbContext.Schedules
                .Include(s => s.MovieSchedules)
                .FirstOrDefaultAsync(s => s.Id == scheduleId);
        }

        public async Task<Schedule> AddScheduleAsync(Schedule schedule)
        {
            await _dbContext.Schedules.AddAsync(schedule);
            return schedule;
        }

        public async Task<Schedule> UpdateScheduleAsync(Schedule schedule)
        {
            _dbContext.Entry(schedule).State = EntityState.Modified;
            return schedule;
        }

        public async Task<Schedule> DeleteScheduleAsync(int scheduleId)
        {
            var schedule = await _dbContext.Schedules.FindAsync(scheduleId);
            if (schedule != null)
            {
                _dbContext.Schedules.Remove(schedule);
            }
            return schedule;
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}