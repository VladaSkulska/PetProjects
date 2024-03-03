using HDrezka.Models.DTOs;
using HDrezka.Services;
using Microsoft.AspNetCore.Mvc;

namespace HDrezka.Controllers
{
    [ApiController]
    [Route("api/schedules")]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public SchedulesController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetAllSchedulesAsync()
        {
            var schedules = await _scheduleService.GetSchedulesAsync();
            return Ok(schedules);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleDto>> GetScheduleByIdAsync(int id)
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            return Ok(schedule);
        }

        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetSchedulesForDateAsync(string date)
        {
            if (!DateTime.TryParse(date, out DateTime parsedDate))
            {
                return BadRequest("Invalid date format. Please provide the date in YYYY-MM-DD format.");
            }

            var schedules = await _scheduleService.GetScheduleForDateAsync(parsedDate);
            return Ok(schedules);
        }

        [HttpPost]
        public async Task<ActionResult<ScheduleDto>> AddScheduleAsync(ScheduleDto scheduleDto)
        {
            try
            {
                var addedSchedule = await _scheduleService.AddScheduleAsync(scheduleDto);
                return CreatedAtAction(nameof(GetScheduleByIdAsync), new { id = addedSchedule.Id }, addedSchedule);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateScheduleAsync(int id, ScheduleDto scheduleDto)
        {
            try
            {
                await _scheduleService.UpdateScheduleAsync(id, scheduleDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteScheduleAsync(int id)
        {
            try
            {
                await _scheduleService.DeleteScheduleAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
