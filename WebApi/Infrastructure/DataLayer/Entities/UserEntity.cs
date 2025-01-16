namespace Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

public class UserEntity : Entity
{
    public string? Name { get; init; }
    
    public string? Surname { get; init; }
    
    [EmailAddress]
    public string Email { get; init; }
    
    [Length(9, 20)]
    public string Password { get; init; }
}