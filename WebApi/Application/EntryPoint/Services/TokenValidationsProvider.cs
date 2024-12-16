using Microsoft.IdentityModel.Tokens;

namespace WebApplication1.Services;

public static class TokenValidationsProvider
{
    public static TokenValidationParameters GetTokenValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey("oursupersecretkey555kokoollpooirwerwerj"u8.ToArray())
        };
    } 
}