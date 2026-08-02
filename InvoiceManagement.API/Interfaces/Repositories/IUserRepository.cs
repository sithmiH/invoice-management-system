using InvoiceManagement.API.Models;

namespace InvoiceManagement.API.Interfaces.Repositories;

// This interface provides methods for retrieve, register, and manage user records
public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);

    Task<User?> GetUserByIdAsync(int id);

    Task<int> RegisterUserAsync(User user);

    Task<IEnumerable<User>> GetAllUsersAsync();
}
