using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class RepeatRepository: Repository<RepeatEntity>
{
    public RepeatRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }
    
    public RepeatEntity? GetById(Guid id)
    {
        return Get().SingleOrDefault(repeatEntity => repeatEntity.Id == id);
    }
}