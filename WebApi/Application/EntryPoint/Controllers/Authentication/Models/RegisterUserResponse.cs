using Core.User;

namespace WebApi.Controllers.User.Models;

public record RegisterUserResponse(RegistrationResult  RegistrationResult, string? AuthToken);