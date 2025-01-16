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

    public List<EventEntity> GetEventByCreatorId(Guid creatorId)
    {
        return eventRepository.GetByCreatorId(creatorId).ToList();
    }

    public EventEntity GetEventById(Guid id)
    {
        return eventRepository.GetById(id);
    }
    
    // public EventEntity CreateEvent()
    // {
    // }
}