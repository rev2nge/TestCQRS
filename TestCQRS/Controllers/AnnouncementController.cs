using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestCQRS.Application.Commands;
using TestCQRS.Application.Queries;

namespace TestCQRS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementController : Controller
    {
        private readonly IMediator _mediator;
        public AnnouncementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAnnouncements")]
        public async Task<IActionResult> GetAnnouncements(string sortBy = "Date", string sortOrder = "asc")
        {
            try
            {
                var announcements = await _mediator.Send(new GetAnnouncementsQuery(sortBy, sortOrder));

                return Ok(new { announcements });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка при получении списка объявлений: " + ex);
            }
        }

        [HttpGet("GetAnnouncement/{id:Guid}")]
        public async Task<IActionResult> GetAnnouncement(Guid id)
        {
            try
            {
                var announce = await _mediator.Send(new GetAnnouncementByIdQuery(id));

                return Ok(new { announce });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка при получении объявления: " + ex);
            }
        }

        [HttpPost("CreateAnnouncement")]
        public async Task<IActionResult> CreateAnnouncement([FromBody] CreateAnnouncementCommand announce)
        {
            try
            {
                var id = await _mediator.Send(announce);

                return Ok(new {id});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка при создании объявления: " + ex);
            }
        }

        [HttpPost("UpdateAnnouncement")]
        public async Task<IActionResult> UpdateAnnouncement([FromBody] UpdateAnnouncementCommand announce)
        {
            try
            {
                var id = await _mediator.Send(announce);

                return Ok(new { id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка при создании объявления: " + ex);
            }
        }

        [HttpGet("SearchAnnouncements")]
        public async Task<IActionResult> SearchAnnouncements([FromQuery] GetSearchAnnouncementResult search)
        {
            var announce = await _mediator.Send(new GetSearchAnnouncementQuery(search));

            return Ok(new { announce });
        }
    }
}
