using InvoiceManagement.API.DTOs.Auth;
using InvoiceManagement.API.Models;

namespace InvoiceManagement.API.Interfaces.Services;

// Defines authentication related operations such as login and registration
public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);

    Task<bool> RegisterAsync(RegisterRequestDto request);

    Task<IEnumerable<User>> GetAllUsersAsync();
}
