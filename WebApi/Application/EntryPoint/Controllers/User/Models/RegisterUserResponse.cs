using Core.User;

namespace WebApplication1.Controllers.User.Models;

public record RegisterUserResponse(RegistrationResult  RegistrationResult, string? AuthToken);