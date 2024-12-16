using Core.User;

namespace WebApplication1.Controllers.User.Models;

public record AuthorizationUserResponse(AuthorizationResult AuthorizationResult, string? AuthToken);