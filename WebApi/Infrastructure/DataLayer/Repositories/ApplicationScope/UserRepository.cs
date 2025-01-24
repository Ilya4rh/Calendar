using Infrastructure.Entities;

namespace Infrastructure.Repositories.ApplicationScope;

public class UserRepository : ApplicationScopeRepository<UserEntity>
{
    public UserRepository(IApplicationScope applicationScope) : base(applicationScope)
    {
    }
    
    public UserEntity? GetByEmailOrDefault(string email)
    {
        return Select().SingleOrDefault(x => x.Email == email);
    }
    
    public UserEntity GetByEmail(string email)
    {
        return Select().Single(x => x.Email == email);
    }
    
    public UserEntity? GetById(Guid id)
    {
        return Select().SingleOrDefault(x => x.Id == id);
    }
}