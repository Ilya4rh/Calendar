using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Infrastructure.Repositories;
using Infrastructure.Repositories.ApplicationScope;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Services;

public class JwtProvider
{
    private readonly UserRepository userRepository;

    public JwtProvider(UserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public string GenerateAuthToken(string email)
    {
        var user = userRepository.GetByEmail(email);
        var claims = new [] {new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())};
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey("oursupersecretkey555kokoollpooirwerwerj"u8.ToArray()),
            SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(claims: claims,signingCredentials: signingCredentials);
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenString;
    }
}