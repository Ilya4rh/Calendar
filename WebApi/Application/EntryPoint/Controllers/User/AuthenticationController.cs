using System.IdentityModel.Tokens.Jwt;
using Core.User;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers.User.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers.User;

[Route("[controller]/[action]")]
[ApiController]
public class AuthenticationController: ControllerBase
{
    private readonly UserService userService;
    private readonly JwtProvider jwtProvider;
    private readonly IHttpContextAccessor context;

    public AuthenticationController(UserService userService, JwtProvider jwtProvider, IHttpContextAccessor context)
    {
        this.userService = userService;
        this.jwtProvider = jwtProvider;
        this.context = context;
    }

    [HttpPost]
    public ActionResult<RegisterUserResponse> Register([FromBody] AuthenticateUserRequest authenticateRequest)
    {
        var registrationResult = userService.RegisterUser(authenticateRequest.Email, authenticateRequest.Password);

        if (registrationResult != RegistrationResult.Success)
            return new RegisterUserResponse(registrationResult, null);
        
        var token = jwtProvider.GenerateAuthToken(authenticateRequest.Email);
        context.HttpContext.Response.Cookies.Append("Auth",token);
        return new RegisterUserResponse(registrationResult, token);
    }
    
    [HttpGet]
    public ActionResult<AuthenticationUserResponse> Authenticate([FromQuery] AuthenticateUserRequest registerRequest)
    {
        var authenticationResult = userService.Authorize(registerRequest.Email, registerRequest.Password);

        if (authenticationResult != AuthenticationResult.Success)
            return new AuthenticationUserResponse(authenticationResult, null);
        
        var token = jwtProvider.GenerateAuthToken(registerRequest.Email);
        context.HttpContext.Response.Cookies.Append("Auth",token);
        return new AuthenticationUserResponse(authenticationResult, token);
    }

    [HttpGet]
    public ActionResult<bool> IsAuthenticated()
    {
        var authToken = context.HttpContext.Request.Cookies["Auth"];
        var handler = new JwtSecurityTokenHandler();
        try
        {
            handler.ValidateToken(authToken, TokenValidationsProvider.GetTokenValidationParameters(), out var token);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}