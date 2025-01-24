using Core.Event.Models;
using Core.Extensions;
using Core.Generator;
using Infrastructure.Repositories.UserScope;

namespace Core.Event;

public class EventService
{
    private readonly EventRepository eventRepository;

    private readonly RepeatRepository repeatRepository;

    private readonly IGenerator<EventDto> generator;

    public EventService(EventRepository eventRepository, RepeatRepository repeatRepository, IGenerator<EventDto> generator)
    {
        this.eventRepository = eventRepository;
        this.repeatRepository = repeatRepository;
        this.generator = generator;
    }

    public List<EventDto> GetEvents()
    {
        var events = eventRepository.Select().AsEnumerable();
        var eventsDto = new List<EventDto>();
        
        foreach (var eventEntity in events)
        {
            var repeat = eventEntity.RepeatId == null ? null : repeatRepository.GetById(eventEntity.RepeatId.Value);
            
            eventsDto.Add(eventEntity.ToDto(repeat?.ToDto()));
        }

        foreach (var eventDto in eventsDto.ToList())
        {
            eventsDto.AddRange(generator.Generate(eventDto));
        }

        return eventsDto;
    }

    public Guid CreateEvent(EventDto eventDto)
    {
        Guid? newRepeatId = null; 
        
        if (eventDto.Repeat != null)
        {
            var newRepeat = repeatRepository.Save(eventDto.Repeat.ToEntity());
            newRepeatId = newRepeat.Id;
        }

        var newEventId = eventRepository.Save(eventDto.ToEntity(newRepeatId));

        return newEventId.Id;
    }

    public EventDto ChangeEvent(EventDto eventDto)
    {
        var repeatDto = eventDto.Repeat;
            
        if (repeatDto != null)
        {
            if (repeatDto.Id == Guid.Empty)
            {
                var newRepeat = repeatRepository.Save(repeatDto.ToEntity());
                repeatDto = newRepeat.ToDto();
            }
            else
            {
                var changedRepeat = repeatRepository.Update(repeatDto.ToEntity());
                repeatDto = changedRepeat.ToDto();
            }
        }

        var changedEvent = eventRepository.Update(eventDto.ToEntity(repeatDto?.Id));

        return changedEvent.ToDto(repeatDto);
    }
}