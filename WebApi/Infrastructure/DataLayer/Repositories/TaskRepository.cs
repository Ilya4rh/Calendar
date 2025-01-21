using Infrastructure.Entities;
using Microsoft.Extensions.Hosting.Internal;

namespace Infrastructure.Repositories;

public class TaskRepository : Repository<TaskEntity>
{
    public TaskRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }

    public TaskEntity GetByTitle(string title, Guid categoryId)
    {
        return Get().Single(x=> x.Title==title && x.CreatorId == categoryId);
    }
}