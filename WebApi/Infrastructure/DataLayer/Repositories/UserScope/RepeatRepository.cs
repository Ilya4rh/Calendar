using Infrastructure.Entities;

namespace Infrastructure.Repositories.UserScope;

public class RepeatRepository: UserScopeRepository<RepeatEntity>
{
    public RepeatRepository(IUserScope userScope) : base(userScope)
    {
    }
    
    public RepeatEntity? GetById(Guid id)
    {
        return Select().SingleOrDefault(repeatEntity => repeatEntity.Id == id);
    }
}