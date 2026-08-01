using Dapper;
using InvoiceManagement.API.Interfaces.Repositories;
using InvoiceManagement.API.Models;
using InvoiceManagement.API.Data;

namespace InvoiceManagement.API.Repositories;

// Repository responsible for performing CRUD operations on the Users table using Dapper as the micro-ORM for data access

public class UserRepository : IUserRepository
{
    private readonly DapperContext _context;

    public UserRepository(DapperContext context)
    {
        _context = context;
    }

    // Retrieves a user by their email address
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var query = @"
            SELECT *
            FROM Users
            WHERE Email = @Email";

        using var connection = _context.CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<User>(
            query,
            new { Email = email });
    }

    // Retrieves a user by their Id
    public async Task<User?> GetUserByIdAsync(int id)
    {
        var query = @"
            SELECT *
            FROM Users
            WHERE Id = @Id";

        using var connection = _context.CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<User>(
            query,
            new { Id = id });
    }

    // Register a new user
    public async Task<int> RegisterUserAsync(User user)
    {
        var query = @"
            INSERT INTO Users
            (
                Name,
                Email,
                PasswordHash,
                Role
            )
            VALUES
            (
                @Name,
                @Email,
                @PasswordHash,
                @Role
            );

            SELECT CAST(SCOPE_IDENTITY() as int);";

        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<int>(query, user);
    }
}
