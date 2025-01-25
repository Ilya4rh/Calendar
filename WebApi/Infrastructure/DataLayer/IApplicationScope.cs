using Infrastructure.Entities;

namespace Infrastructure;

public interface IApplicationScope
{
    public TEntity Save<TEntity>(TEntity entity) where TEntity : Entity;

    public TEntity Update<TEntity>(TEntity entity) where TEntity : Entity;

    public IQueryable<TEntity> Select<TEntity>() where TEntity : Entity;
    
    public void Delete<TEntity>(TEntity entity) where TEntity : Entity;
}