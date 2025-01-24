using Core.Event;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers.Event.Models.Requests;
using WebApplication1.Controllers.Event.Models.Responses;

namespace WebApplication1.Controllers.Event;

[Route("[controller]/[action]")]
[ApiController]
public class EventController
{
    private readonly EventService eventService;

    public EventController(EventService eventService)
    {
        this.eventService = eventService;
    }

    [HttpGet]
    public ActionResult<List<EventResponse>> GetEventsByCreatorId([FromQuery] Guid creatorId)
    {
        return new List<EventResponse>();
    }

    [HttpGet]
    public ActionResult<EventResponse> GetEventById([FromQuery] Guid id)
    {
        return new EventResponse
        {
            Id = default,
            CreatorId = default,
            StartDateTime = default,
            EndDateTime = default,
            Repeat = 0,
            Description = null,
            GuestIds = Array.Empty<Guid>()
        };
    }

    [HttpPost]
    public ActionResult<Guid> CreateEvent([FromBody] CreateEventRequest request)
    {
        return Guid.Empty;
    }

    [HttpPut]
    public ActionResult<EventResponse> ChangeEvent([FromBody] ChangeEventRequest request)
    {
        return new EventResponse
        {
            Id = default,
            CreatorId = default,
            StartDateTime = default,
            EndDateTime = default,
            Repeat = 0,
            Description = null,
            GuestIds = Array.Empty<Guid>()
        };
    }

    [HttpDelete]
    public ActionResult<bool> DeleteEvent([FromQuery] Guid eventId)
    {
        return true;
    }
}