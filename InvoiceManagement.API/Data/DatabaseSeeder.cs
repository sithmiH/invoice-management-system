using BCrypt.Net;
using InvoiceManagement.API.Interfaces.Repositories;
using InvoiceManagement.API.Models;

namespace InvoiceManagement.API.Data;

// Seeds a default Admin account
public class DatabaseSeeder
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly ILogger<DatabaseSeeder> _logger;

    public DatabaseSeeder(
        IUserRepository userRepository,
        IConfiguration configuration,
        ILogger<DatabaseSeeder> logger)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _logger = logger;
    }

    //  Creates the default Admin account if account doesn't already exist
    public async Task SeedAdminAsync()
    {
        var email = _configuration["DefaultAdmin:Email"];

        var existingAdmin = await _userRepository.GetUserByEmailAsync(email!);

        if (existingAdmin != null)
        {
            _logger.LogInformation("Default admin already exists.");
            return;
        }

        var admin = new User
        {
            Name = _configuration["DefaultAdmin:Name"]!,
            Email = email!,
            Password = BCrypt.Net.BCrypt.HashPassword(
                _configuration["DefaultAdmin:Password"]!),
            Role = "Admin"
        };

        await _userRepository.RegisterUserAsync(admin);

        _logger.LogInformation("Default admin account created successfully.");
    }
}
