using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public abstract class Repository<TEntity> where TEntity: Entity
{
    private readonly ApplicationContext _applicationContext;

    public Repository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    public TEntity Save(TEntity entity)
    {
        var context = new ValidationContext(entity);
        var results = new List<ValidationResult>();
        
        if (!Validator.TryValidateObject(entity, context, results, true))
            throw new ValidationException();
        
        var entry = _applicationContext.Add(entity);
        
        _applicationContext.SaveChanges();
        
        return entry.Entity;
    }
    
    public TEntity Update(TEntity entity)
    {
        var context = new ValidationContext(entity);
        var results = new List<ValidationResult>();

        if (!Validator.TryValidateObject(entity, context, results, true))
            throw new ValidationException();
        
        var trackedEntity = _applicationContext.Set<TEntity>().Local.FirstOrDefault(e => e.Id == entity.Id);
        if (trackedEntity != null)
        {
            _applicationContext.Entry(trackedEntity).CurrentValues.SetValues(entity);
        }
        else
        {
            _applicationContext.Attach(entity);
            _applicationContext.Entry(entity).State = EntityState.Modified;
        }

        _applicationContext.SaveChanges();

        return entity;
    }
    
    protected IQueryable<TEntity> Get()
    {
        return _applicationContext.Set<TEntity>();
    }
}