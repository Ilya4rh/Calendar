using Infrastructure.Entities;

namespace Infrastructure;

public interface IUserScope
{
    public TEntity Save<TEntity>(TEntity entity) where TEntity : UserScopeEntity;

    public TEntity Update<TEntity>(TEntity entity) where TEntity : UserScopeEntity;

    public IQueryable<TEntity> Select<TEntity>() where TEntity : UserScopeEntity;

    public void Delete<TEntity>(TEntity entity) where TEntity : UserScopeEntity;
}