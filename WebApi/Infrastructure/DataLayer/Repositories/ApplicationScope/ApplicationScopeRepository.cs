using Infrastructure.Entities;

namespace Infrastructure.Repositories.ApplicationScope;

public abstract class ApplicationScopeRepository<TEntity> where TEntity: Entity
{
    private readonly IApplicationScope applicationScope;

    protected ApplicationScopeRepository(IApplicationScope applicationScope)
    {
        this.applicationScope = applicationScope;
    }
    
    public TEntity Save(TEntity entity)
    {
        return applicationScope.Save(entity);
    }
    
    public TEntity Update(TEntity entity)
    {
        return applicationScope.Update(entity);
    }
    
    protected IQueryable<TEntity> Select()
    {
        return applicationScope.Select<TEntity>();
    }
}