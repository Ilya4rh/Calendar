using Core.User;

namespace EntryPoint.Controllers.Authentication.Models;

public record AuthenticationUserResponse(AuthenticationResult AuthenticationResult, string? AuthToken);