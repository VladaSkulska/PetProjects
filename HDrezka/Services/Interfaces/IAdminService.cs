using HDrezka.Models.DTOs.Identity;

namespace HDrezka.Services.Interfaces
{
    public interface IAdminService
    {
        Task RegisterAdminAsync(RegisterModel model);
    }
}
