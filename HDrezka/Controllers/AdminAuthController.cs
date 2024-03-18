using HDrezka.Models.DTOs.Identity;
using HDrezka.Services.Interfaces;
using HDrezka.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HDrezka.Controllers
{
    [ApiController]
    [Route("api/admin/auth")]
    public class AdminAuthController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminAuthController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            try
            {
                await _adminService.RegisterAdminAsync(model);
                return Ok(new Response { Status = "Success", Message = "Admin created successfully!" });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = ex.Message });
            }
        }
    }
}