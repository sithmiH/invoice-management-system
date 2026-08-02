using InvoiceManagement.Web.DTOs.Auth;

namespace InvoiceManagement.Web.Services;

// Defines the contract for calling the backend Auth API
public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);

    Task<bool> RegisterAsync(RegisterRequestDto request);
}
