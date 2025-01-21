using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace Core.Event;

public class EventService
{
    private readonly EventRepository eventRepository;

    public EventService(EventRepository eventRepository)
    {
        this.eventRepository = eventRepository;
    }

    public List<EventEntity> GetEventsByCreatorId(Guid creatorId)
    {
        return eventRepository.GetByCreatorId(creatorId).ToList();
    }

    public EventEntity GetEventById(Guid id)
    {
        return eventRepository.GetById(id);
    }
    
    public Guid CreateEvent(EventEntity eventEntity)
    {
        var newEvent = eventRepository.Save(eventEntity);

        return newEvent.Id;
    }

    public EventEntity ChangeEvent(EventEntity eventEntity)
    {
        var changedEvent = eventRepository.Update(eventEntity);

        return changedEvent;
    }
}