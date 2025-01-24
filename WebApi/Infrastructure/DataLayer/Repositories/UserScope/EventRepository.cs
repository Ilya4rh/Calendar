using Infrastructure.Entities;

namespace Infrastructure.Repositories.UserScope;

public class EventRepository : UserScopeRepository<EventEntity>
{
    public EventRepository(IUserScope userScope) : base(userScope)
    {
    }

    public EventEntity GetById(Guid id)
    {
        return Select().Single(eventEntity => eventEntity.Id == id);
    }
}