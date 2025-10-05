using Microsoft.AspNetCore.Mvc;
using ProjectsComposer.API.Services;
using ProjectsComposer.Core.Contracts.Authorization;

namespace ProjectsComposer.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(AccountsService accountsService) : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterUserRequest request)
    {
        accountsService.Register(request.UserName, request.Password);
        return NoContent();
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        return Ok(accountsService.Login(request.Username, request.Password));
    }
}