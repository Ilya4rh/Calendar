using Core.Event;
using Core.Event.Models;
using Microsoft.AspNetCore.Mvc;

namespace EntryPoint.Controllers.Event;

[Route("[controller]/[action]")]
[ApiController]
public class EventController: ControllerBase
{
    private readonly EventService eventService;

    public EventController(EventService eventService)
    {
        this.eventService = eventService;
    }

    [HttpGet]
    public ActionResult<List<EventDto>> GetEventsForYear()
    {
        var events = eventService.GetEvents();
        
        return events;
    }

    [HttpPost]
    public ActionResult<Guid> CreateEvent([FromBody] EventDto request)
    {
        var idNewEvent = eventService.CreateEvent(request);
        
        return idNewEvent;
    }

    [HttpPut]
    public ActionResult<EventDto> ChangeEvent([FromBody] EventDto request)
    {
        var changedEvent = eventService.ChangeEvent(request);
        
        return changedEvent;
    }

    [HttpDelete]
    public ActionResult DeleteEvent([FromQuery] Guid eventId)
    {
        return Ok();
    }
}