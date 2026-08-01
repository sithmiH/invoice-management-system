namespace InvoiceManagement.API.Interfaces.Services;

// Defines the operations for generating JWT tokens
public interface IJwtService
{
    string GenerateToken(int userId, string email, string role);
}
