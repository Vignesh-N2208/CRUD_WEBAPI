// Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using CRUD_WEBAPI.DTOs;
using CRUD_WEBAPI.Services;

namespace CRUD_WEBAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);
        if (result == null)
        {
            return Conflict("Username is already taken.");
        }
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        if (result == null)
        {
            return Unauthorized("Invalid username or password.");
        }
        return Ok(result);
    }
}