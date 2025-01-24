namespace EntryPoint.Controllers.Authentication.Models;

public record AuthenticateUserRequest(string Email, string Password);