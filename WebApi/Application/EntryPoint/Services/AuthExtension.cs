using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApi.Services;

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