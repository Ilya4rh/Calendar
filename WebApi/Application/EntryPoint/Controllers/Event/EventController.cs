using Core.Event;
using Core.Event.Models;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers.Event.Models.Requests;

namespace WebApplication1.Controllers.Event;

[Route("[controller]/[action]")]
[ApiController]
public class EventController
{
    private readonly EventService _eventService;

    public EventController(EventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public ActionResult<List<EventDto>> GetEventsByCreatorId([FromQuery] Guid creatorId)
    {
        var events = _eventService.GetEventsByCreatorId(creatorId);
        
        return events;
    }

    [HttpGet]
    public ActionResult<EventDto> GetEventById([FromQuery] Guid id)
    {
        var eventDto = _eventService.GetEventById(id);
        
        return eventDto;
    }

    [HttpPost]
    public ActionResult<Guid> CreateEvent([FromBody] CreateEventRequest request)
    {
        var eventForCreate = new EventEntity
        {
            CreatorId = request.CreatorId,
            Title = request.Title,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            RepeatId = request.Repeat?.Id,
            Description = request.Description,
            GuestIds = request.GuestIds
        };

        var idNewEvent = _eventService.CreateEvent(eventForCreate);
        
        return idNewEvent;
    }

    [HttpPut]
    public ActionResult<EventEntity> ChangeEvent([FromBody] ChangeEventRequest request)
    {
        var eventForChange = new EventEntity
        {
            Id = request.Id,
            CreatorId = request.CreatorId,
            Title = request.Title,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            RepeatId = request.Repeat?.Id,
            Description = request.Description,
            GuestIds = request.GuestIds
        };

        var changedEvent = _eventService.ChangeEvent(eventForChange);
        
        return changedEvent;
    }

    [HttpDelete]
    public ActionResult<bool> DeleteEvent([FromQuery] Guid eventId)
    {
        return true;
    }
}