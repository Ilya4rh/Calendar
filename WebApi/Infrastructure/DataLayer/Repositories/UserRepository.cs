using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class UserRepository : Repository<UserEntity>
{
    public UserRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }
    
    public UserEntity? GetByEmailOrDefault(string email)
    {
        return Get().SingleOrDefault(x => x.Email == email);
    }
    
    public UserEntity GetByEmail(string email)
    {
        return Get().Single(x => x.Email == email);
    }
}