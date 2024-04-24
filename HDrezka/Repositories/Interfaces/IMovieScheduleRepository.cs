using HDrezka.Models;

namespace HDrezka.Repositories
{
    public interface IMovieScheduleRepository
    {
        Task<MovieSchedule> GetMovieScheduleByIdAsync(int movieScheduleId);
    }
}