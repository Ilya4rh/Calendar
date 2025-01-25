namespace Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

public class UserEntity : Entity
{
    [EmailAddress]
    public string Email { get; init; }
    
    [Length(9, 20)]
    public string Password { get; init; }
}