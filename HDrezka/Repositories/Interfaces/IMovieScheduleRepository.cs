using HDrezka.Models;

namespace HDrezka.Repositories.Interfaces
{
    public interface IMovieScheduleRepository
    {
        Task<MovieSchedule> GetMovieScheduleByIdAsync(int movieScheduleId);
    }
}