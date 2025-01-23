using Core.Event;
using Core.Event.Models;
using Core.Repeat.Models;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers.Event.Models.EventModels.Requests;

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
    public ActionResult<List<EventDto>> GetEventsByCreatorIdForYear([FromQuery] Guid creatorId)
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
        RepeatDto? repeatDto = null;

        if (request.RepeatRequest != null)
        {
            var repeatRequest = request.RepeatRequest;
            repeatDto = new RepeatDto
            {
                DateStart = repeatRequest.DateStart,
                DateEnd = repeatRequest.DateEnd,
                Interval = repeatRequest.Interval,
                IntervalType = repeatRequest.IntervalType
            };
        }
        
        var eventDtoForCreate = new EventDto
        {
            CreatorId = request.CreatorId,
            Title = request.Title,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            Repeat = repeatDto,
            GuestIds = request.GuestIds
        };

        var idNewEvent = _eventService.CreateEvent(eventDtoForCreate);
        
        return idNewEvent;
    }

    [HttpPut]
    public ActionResult<EventDto> ChangeEvent([FromBody] ChangeEventRequest request)
    {
        RepeatDto? repeatDto = null;

        if (request.RepeatRequest != null)
        {
            var repeatRequest = request.RepeatRequest;

            if (repeatRequest.Id == null)
            {
                repeatDto = new RepeatDto
                {
                    DateStart = repeatRequest.DateStart,
                    DateEnd = repeatRequest.DateEnd,
                    Interval = repeatRequest.Interval,
                    IntervalType = repeatRequest.IntervalType
                };
            }
            else
            {
                repeatDto = new RepeatDto
                {
                    Id = repeatRequest.Id.Value,
                    DateStart = repeatRequest.DateStart,
                    DateEnd = repeatRequest.DateEnd,
                    Interval = repeatRequest.Interval,
                    IntervalType = repeatRequest.IntervalType
                };
            }
            
        }
        
        var eventForChange = new EventDto
        {
            Id = request.Id,
            CreatorId = request.CreatorId,
            Title = request.Title,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            Repeat = repeatDto,
            GuestIds = request.GuestIds,
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