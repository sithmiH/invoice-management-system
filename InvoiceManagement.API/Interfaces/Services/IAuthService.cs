using InvoiceManagement.API.DTOs.Auth;

namespace InvoiceManagement.API.Interfaces.Services;

public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);

    Task<bool> RegisterAsync(RegisterRequestDto request);
}
