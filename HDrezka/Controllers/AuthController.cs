using HDrezka.Services.Interfaces;
using HDrezka.Models.DTOs.Identity;
using Microsoft.AspNetCore.Mvc;
using HDrezka.Utilities;
using System.IdentityModel.Tokens.Jwt;
using HDrezka.Utilities.Exceptions;
using HDrezka.Models;

namespace HDrezka.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authenticationService;
        public AuthController(IAuthService authenticationService) 
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                await _authenticationService.RegisterUserAsync(model);
                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            }
            catch (UserRegistrationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var token = await _authenticationService.LoginAsync(model.Username, model.Password);
            if (token == null)
            {
                return Unauthorized();
            }

            var response = new LoginResponseModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };

            return Ok(response);
        }
    }
}