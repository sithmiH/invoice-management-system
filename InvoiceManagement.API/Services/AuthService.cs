using BCrypt.Net;
using InvoiceManagement.API.DTOs.Auth;
using InvoiceManagement.API.Interfaces.Repositories;
using InvoiceManagement.API.Interfaces.Services;
using InvoiceManagement.API.Models;

namespace InvoiceManagement.API.Services;

// Handles user authentication logic
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    // Service responsible for generating JWT tokens
    public AuthService(
        IUserRepository userRepository,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    // Registers a new user
    public async Task<bool> RegisterAsync(RegisterRequestDto request)
    {
        // Check whether the email already exists
        var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);

        if (existingUser != null)
        {
            return false;
        }

        // Create the user
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = "User"
        };

        var userId = await _userRepository.RegisterUserAsync(user);

        return userId > 0;
    }

    // Authenticates a user by email and password and issues a jwt token
    public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto request)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email);

        if (user == null)
        {
            return null;
        }

        var isPasswordValid =
            BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

        if (!isPasswordValid)
        {
            return null;
        }

        var token = _jwtService.GenerateToken(
            user.Id,
            user.Email,
            user.Role);

        return new AuthResponseDto
        {
            Token = token,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }
}
