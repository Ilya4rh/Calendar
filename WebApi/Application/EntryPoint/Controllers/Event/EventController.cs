using Core.Event.Interfaces;
using Core.Event.Models;
using Microsoft.AspNetCore.Mvc;

namespace EntryPoint.Controllers.Event;

[Route("[controller]/[action]")]
[ApiController]
public class EventController: ControllerBase
{
    private readonly IEventService eventService;
    
    public EventController(IEventService eventService)
    {
        this.eventService = eventService;
    }

    [HttpGet]
    public ActionResult<List<EventDto>> GetEventsForYear([FromQuery] int year)
    {
        var events = eventService.GetEventsForYear(year);
        
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
        eventService.DeleteEvent(eventId);
        
        return Ok();
    }
}