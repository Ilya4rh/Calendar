using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace Core.User;

public class UserService
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
    
    public AuthorizationResult Authorize(string email, string password)
    {
        var user = userRepository.GetByEmailOrDefault(email);

        if (user is null)
            return AuthorizationResult.UserNotFound;

        return user.Password != password
            ? AuthorizationResult.WrongPassword
            : AuthorizationResult.Success;
    }
}
