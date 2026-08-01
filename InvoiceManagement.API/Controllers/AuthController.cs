using InvoiceManagement.API.DTOs.Auth;
using InvoiceManagement.API.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagement.API.Controllers;

// Handles authentication related HTTP endpoints like registration and login
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // Registers a new user account
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto request)
    {
        var success = await _authService.RegisterAsync(request);

        if (!success)
        {
            return BadRequest(new
            {
                message = "Email already exists."
            });
        }

        return Ok(new
        {
            message = "User registered successfully."
        });
    }

    // Authenticates a user and returns a JWT token when login is successful
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var response = await _authService.LoginAsync(request);

        if (response == null)
        {
            return Unauthorized(new
            {
                message = "Invalid email or password."
            });
        }

        return Ok(response);
    }
}
