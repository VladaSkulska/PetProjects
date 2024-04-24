using HDrezka.Models.DTOs.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace HDrezka.Services.Interfaces
{
    public interface IAuthService
    {
        Task RegisterUserAsync(RegisterModel model);
        Task<JwtSecurityToken> LoginAsync(string username, string password);
    }
}