using Core.User;

namespace WebApplication1.Controllers.User.Models;

public record AuthenticationUserResponse(AuthenticationResult AuthenticationResult, string? AuthToken);