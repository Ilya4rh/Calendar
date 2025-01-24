using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationScope : IApplicationScope
{
    private readonly ApplicationContext applicationContext;

    public ApplicationScope(ApplicationContext applicationContext)
    {
        this.applicationContext = applicationContext;
    }

    public TEntity Save<TEntity>(TEntity entity) where TEntity: Entity
    {
        var context = new ValidationContext(entity);
        var results = new List<ValidationResult>();
        
        if (!Validator.TryValidateObject(entity, context, results, true))
            throw new ValidationException();
        
        var newEntity = applicationContext.Add(entity).Entity;
        
        applicationContext.SaveChanges();
        
        return newEntity;
    }
    
    public TEntity Update<TEntity>(TEntity entity) where TEntity: Entity
    {
        var context = new ValidationContext(entity);
        var results = new List<ValidationResult>();

        if (!Validator.TryValidateObject(entity, context, results, true))
            throw new ValidationException();

        var newEntity = applicationContext.Update(entity).Entity;
        
        applicationContext.SaveChanges();

        return newEntity;
    }
    
    public IQueryable<TEntity> Select<TEntity>() where TEntity: Entity
    {
        return applicationContext.Set<TEntity>().AsNoTracking();
    }
}