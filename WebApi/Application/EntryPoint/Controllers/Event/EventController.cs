using Core.Event;
using Infrastructure.Entities;
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
    public ActionResult<List<EventEntity>> GetEventsByCreatorId([FromQuery] Guid creatorId)
    {
        var events = eventService.GetEventsByCreatorId(creatorId);
        
        return events;
    }

    [HttpGet]
    public ActionResult<EventEntity> GetEventById([FromQuery] Guid id)
    {
        var eventById = eventService.GetEventById(id);
        
        return eventById;
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
            RepeatId = request.RepeatId,
            Description = request.Description,
            GuestIds = request.GuestIds
        };

        var idNewEvent = eventService.CreateEvent(eventForCreate);
        
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
            RepeatId = request.RepeatId,
            Description = request.Description,
            GuestIds = request.GuestIds
        };

        var changedEvent = eventService.ChangeEvent(eventForChange);
        
        return changedEvent;
    }

    [HttpDelete]
    public ActionResult<bool> DeleteEvent([FromQuery] Guid eventId)
    {
        return true;
    }
}