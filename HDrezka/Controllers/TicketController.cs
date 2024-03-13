using HDrezka.Models;
using HDrezka.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HDrezka.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/tickets")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly UserManager<IdentityUser> _userManager;

        public TicketController(ITicketService ticketService, UserManager<IdentityUser> userManager)
        {
            _ticketService = ticketService;
            _userManager = userManager;
        }

        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseTicket([FromBody] PurchaseTicketModel model)
        {
            try
            {
                var ticket = await _ticketService.BuyTicketAsync(model.MovieScheduleId, model.SeatNumber);
                return Ok(ticket);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}