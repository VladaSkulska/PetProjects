using HDrezka.Models.DTOs.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace HDrezka.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task RegisterUserAsync(RegisterModel model);
        Task RegisterAdminAsync(RegisterModel model);
        Task<JwtSecurityToken> LoginAsync(string username, string password);
    }
}