using Core.Event.Models;
using Core.Extensions;
using Core.Generator;
using Infrastructure.Repositories;

namespace Core.Event;

public class EventService
{
    private readonly EventRepository _eventRepository;

    private readonly RepeatRepository _repeatRepository;

    private readonly IGenerator<EventDto> _generator;

    public EventService(EventRepository eventRepository, RepeatRepository repeatRepository, IGenerator<EventDto> generator)
    {
        _eventRepository = eventRepository;
        _repeatRepository = repeatRepository;
        _generator = generator;
    }

    public List<EventDto> GetEventsByCreatorId(Guid creatorId)
    {
        var events = _eventRepository.GetByCreatorId(creatorId).ToList();
        var eventsDto = new List<EventDto>();
        
        foreach (var eventEntity in events)
        {
            var repeat = eventEntity.RepeatId == null ? null : _repeatRepository.GetById(eventEntity.RepeatId.Value);
            
            eventsDto.Add(eventEntity.ToDto(repeat?.ToDto()));
        }

        foreach (var eventDto in eventsDto.ToList())
        {
            eventsDto.AddRange(_generator.Generate(eventDto));
        }

        return eventsDto;
    }

    public EventDto GetEventById(Guid id)
    {
        var eventEntity = _eventRepository.GetById(id);

        var repeat = eventEntity.RepeatId == null ? null : _repeatRepository.GetById(eventEntity.RepeatId.Value);
        
        return eventEntity.ToDto(repeat?.ToDto());
    }

    public Guid CreateEvent(EventDto eventDto)
    {
        Guid? newRepeatId = null; 
        
        if (eventDto.Repeat != null)
        {
            var newRepeat = _repeatRepository.Save(eventDto.Repeat.ToEntity());
            newRepeatId = newRepeat.Id;
        }

        var newEventId = _eventRepository.Save(eventDto.ToEntity(newRepeatId));

        return newEventId.Id;
    }

    public EventDto ChangeEvent(EventDto eventDto)
    {
        var repeatDto = eventDto.Repeat;
            
        if (repeatDto != null)
        {
            if (repeatDto.Id == Guid.Empty)
            {
                var newRepeat = _repeatRepository.Save(repeatDto.ToEntity());
                repeatDto = newRepeat.ToDto();
            }
            else
            {
                var changedRepeat = _repeatRepository.Update(repeatDto.ToEntity());
                repeatDto = changedRepeat.ToDto();
            }
        }

        var changedEvent = _eventRepository.Update(eventDto.ToEntity(repeatDto?.Id));

        return changedEvent.ToDto(repeatDto);
    }
}