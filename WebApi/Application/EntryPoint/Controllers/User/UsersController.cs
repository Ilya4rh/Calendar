using System.IdentityModel.Tokens.Jwt;
using Core.User;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers.User.Models;
using WebApplication1.Services;
using AuthorizationResult = Core.User.AuthorizationResult;

namespace WebApplication1.Controllers.User;

[Route("[controller]/[action]")]
[ApiController]
public class UsersController: ControllerBase
{
    private readonly UserService userService;
    private readonly JwtProvider jwtProvider;
    private readonly IHttpContextAccessor context;

    public UsersController(UserService userService, JwtProvider jwtProvider, IHttpContextAccessor context)
    {
        this.userService = userService;
        this.jwtProvider = jwtProvider;
        this.context = context;
    }

    [HttpPost]
    public ActionResult<RegisterUserResponse> Register([FromBody] AuthorizeUserRequest authorizeRequest)
    {
        var registrationResult = userService.RegisterUser(authorizeRequest.Email, authorizeRequest.Password);

        if (registrationResult != RegistrationResult.Success)
            return new RegisterUserResponse(registrationResult, null);
        
        var token = jwtProvider.GenerateAuthToken(authorizeRequest.Email);
        context.HttpContext.Response.Cookies.Append("Auth",token);
        return new RegisterUserResponse(registrationResult, token);
    }
    
    [HttpGet]
    public ActionResult<AuthorizationUserResponse> Authorize([FromQuery] AuthorizeUserRequest registerRequest)
    {
        var authorizationResult = userService.Authorize(registerRequest.Email, registerRequest.Password);

        if (authorizationResult != AuthorizationResult.Success)
            return new AuthorizationUserResponse(authorizationResult, null);
        
        var token = jwtProvider.GenerateAuthToken(registerRequest.Email);
        context.HttpContext.Response.Cookies.Append("Auth",token);
        return new AuthorizationUserResponse(authorizationResult, token);
    }

    [HttpGet]
    public ActionResult<bool> IsAuthorized()
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