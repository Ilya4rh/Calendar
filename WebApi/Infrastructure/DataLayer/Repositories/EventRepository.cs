using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class EventRepository : Repository<EventEntity>
{
    public EventRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }

    public IQueryable<EventEntity> GetByCreatorId(Guid creatorId)
    {
        return Get().Where(eventEntity => eventEntity.CreatorId == creatorId);
    }

    public EventEntity GetById(Guid id)
    {
        return Get().Single(eventEntity => eventEntity.Id == id);
    }
}