using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Services;

namespace Infrastructure;

public static class AuthExtension
{
    public static void AddAuth(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = TokenValidationsProvider.GetTokenValidationParameters();
            
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies["Auth"];
                    return Task.CompletedTask;
                }
            };
        });
        services.AddAuthorization();
    }
}