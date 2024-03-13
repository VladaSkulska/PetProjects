using HDrezka.Services.Interfaces;
using HDrezka.Models.DTOs.Identity;
using Microsoft.AspNetCore.Mvc;
using HDrezka.Utilities;
using System.IdentityModel.Tokens.Jwt;

namespace HDrezka.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService) 
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
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            try
            {
                await _authenticationService.RegisterAdminAsync(model);
                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            }
            catch (InvalidOperationException ex)
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

            return Ok(new 
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
    }
}