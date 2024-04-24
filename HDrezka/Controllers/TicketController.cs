using HDrezka.Models;
using HDrezka.Services.Interfaces;
using HDrezka.Utilities;
using HDrezka.Utilities.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
            catch (TicketOperationException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound(new Response { Status = "Error", Message = ex.Message });
                }
                else if (ex.StatusCode == HttpStatusCode.BadRequest)
                {
                    return BadRequest(new Response { Status = "Error", Message = ex.Message });
                }

                return StatusCode(500, new Response { Status = "Error", Message = "An unexpected error occurred." });
            }
        }
    }
}