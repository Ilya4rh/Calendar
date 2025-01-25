using Core.User;

namespace EntryPoint.Controllers.Authentication.Models;

public record RegisterUserResponse(RegistrationResult  RegistrationResult, string? AuthToken);