using InvoiceManagement.API.Models;

namespace InvoiceManagement.API.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);

    Task<User?> GetUserByIdAsync(int id);

    Task<int> RegisterUserAsync(User user);
}
