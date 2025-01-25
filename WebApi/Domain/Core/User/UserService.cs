using Core.User.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Repositories.ApplicationScope;

namespace Core.User;

public class UserService : IUserService
{
    private readonly UserRepository userRepository;

    public UserService(UserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public RegistrationResult RegisterUser(string email, string password)
    {
        var userEntity = new UserEntity
        {
            Password = password,
            Email = email,
        };
        
        if (userRepository.GetByEmailOrDefault(email) is not null)
            return RegistrationResult.AlreadyExist;
        
        userRepository.Save(userEntity);
        return RegistrationResult.Success;
    }
    
    public AuthenticationResult Authorize(string email, string password)
    {
        var user = userRepository.GetByEmailOrDefault(email);

        if (user is null)
            return AuthenticationResult.UserNotFound;

        return user.Password != password
            ? AuthenticationResult.WrongPassword
            : AuthenticationResult.Success;
    }
}
