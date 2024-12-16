namespace Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

public class UserEntity : Entity
{
    public string? Name { get; set; }
    
    public string? Surname { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    [Length(9, 20)]
    public string Password { get; set; }
}