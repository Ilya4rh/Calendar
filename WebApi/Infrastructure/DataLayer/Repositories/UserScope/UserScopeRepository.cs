using Infrastructure.Entities;

namespace Infrastructure.Repositories.UserScope;

public abstract class UserScopeRepository<TEntity> where TEntity: UserScopeEntity
{
    private readonly IUserScope userScope;

    protected UserScopeRepository(IUserScope userScope)
    {
        this.userScope = userScope;
    }
    
    public TEntity Save(TEntity entity)
    {
        return userScope.Save(entity);
    }
    
    public TEntity Update(TEntity entity)
    {
        return userScope.Update(entity);
    }

    protected IQueryable<TEntity> Select()
    {
        return userScope.Select<TEntity>();
    }

    public void Delete(TEntity entity)
    {
        userScope.Delete(entity);
    }
}