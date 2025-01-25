namespace Core.User.Interfaces;

public interface IUserService
{
    RegistrationResult RegisterUser(string email, string password);

    AuthenticationResult Authorize(string email, string password);
}