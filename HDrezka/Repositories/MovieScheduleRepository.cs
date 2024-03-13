using HDrezka.Data;
using HDrezka.Models;
using HDrezka.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HDrezka.Repositories
{
    public class MovieScheduleRepository : IMovieScheduleRepository
    {
        private readonly AppDbContext _bdContext;

        public MovieScheduleRepository(AppDbContext context)
        {
            _bdContext = context;
        }

        public async Task<MovieSchedule> GetMovieScheduleByIdAsync(int movieScheduleId)
        {
            var schedule = await _bdContext.Schedules
                                .Include(s => s.MovieSchedules) 
                                .FirstOrDefaultAsync(s => s.MovieSchedules.Any(ms => ms.Id == movieScheduleId));

            var movieSchedule = schedule?.MovieSchedules.FirstOrDefault(ms => ms.Id == movieScheduleId);

            return movieSchedule;
        }
    }
}
