using System.ComponentModel.DataAnnotations;
using Infrastructure.Entities;

namespace Infrastructure;

public abstract class Repository<TEntity> where TEntity: Entity
{
    private readonly ApplicationContext applicationContext;

    public Repository(ApplicationContext applicationContext)
    {
        this.applicationContext = applicationContext;
    }
    
    public TEntity Save(TEntity entity)
    {
        var context = new ValidationContext(entity);
        var results = new List<ValidationResult>();
        
        if (!Validator.TryValidateObject(entity, context, results, true))
            throw new ValidationException();
        var entry = applicationContext.Add(entity);
        applicationContext.SaveChanges();
        return entry.Entity;
    }

    public IQueryable<TEntity> Get()
    {
        return applicationContext.Set<TEntity>();
    }
}