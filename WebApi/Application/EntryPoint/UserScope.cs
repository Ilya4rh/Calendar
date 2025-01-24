using System.IdentityModel.Tokens.Jwt;
using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Repositories.ApplicationScope;

namespace EntryPoint;

public class UserScope: IUserScope
{
    private readonly Guid? userId;
    private readonly IApplicationScope applicationScope;
    
    
    public UserScope(IHttpContextAccessor context, UserRepository userRepository, IApplicationScope applicationScope)
    {
        this.applicationScope = applicationScope;
        try
        {
            var authToken = context.HttpContext.Request.Cookies["Auth"];
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(authToken);
            var userIdFromToken = jwtToken.Subject;
            userRepository.GetById(Guid.Parse(userIdFromToken));
            userId = Guid.Parse(userIdFromToken);
        }
        catch (Exception)
        {
            userId = null;
        }
    }

    public TEntity Save<TEntity>(TEntity entity) where TEntity: UserScopeEntity
    {
        entity.UserId = GetUserId();
        return applicationScope.Save(entity);
    }
    
    public TEntity Update<TEntity>(TEntity entity) where TEntity: UserScopeEntity
    {
        entity.UserId = GetUserId();
        return applicationScope.Update(entity);
    }
    
    public IQueryable<TEntity> Select<TEntity>() where TEntity: UserScopeEntity
    {
        var userId = GetUserId();
        return applicationScope.Select<TEntity>().Where(x => x.UserId == userId);
    }

    private Guid GetUserId()
    {
        if (userId is null)
            throw new Exception("Нет контекста");
        return userId.Value;
    }
}