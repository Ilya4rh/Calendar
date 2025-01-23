using Core.Event.Models;
using Core.Extensions;
using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace Core.Event;

public class EventService
{
    private readonly EventRepository _eventRepository;

    private readonly RepeatRepository _repeatRepository;

    public EventService(EventRepository eventRepository, RepeatRepository repeatRepository)
    {
        _eventRepository = eventRepository;
        _repeatRepository = repeatRepository;
    }

    public List<EventDto> GetEventsByCreatorId(Guid creatorId)
    {
        var events = _eventRepository.GetByCreatorId(creatorId).ToList();
        var eventsDto = new List<EventDto>();
        
        foreach (var eventEntity in events)
        {
            var repeat = eventEntity.RepeatId == null ? null : _repeatRepository.GetById(eventEntity.RepeatId.Value);
            
            eventsDto.Add(eventEntity.ConvertToEventDto(repeat));
        }

        return eventsDto;
    }

    public EventDto GetEventById(Guid id)
    {
        var eventEntity = _eventRepository.GetById(id);

        var repeat = eventEntity.RepeatId == null ? null : _repeatRepository.GetById(eventEntity.RepeatId.Value);
        
        return eventEntity.ConvertToEventDto(repeat);
    }
    
    public Guid CreateEvent(EventEntity eventEntity)
    {
        var newEvent = _eventRepository.Save(eventEntity);

        return newEvent.Id;
    }

    public EventEntity ChangeEvent(EventEntity eventEntity)
    {
        var changedEvent = _eventRepository.Update(eventEntity);

        return changedEvent;
    }
}