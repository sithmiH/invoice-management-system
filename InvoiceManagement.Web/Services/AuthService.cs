using System.Text;
using System.Text.Json;
using InvoiceManagement.Web.DTOs.Auth;
using Microsoft.Extensions.Options;
using InvoiceManagement.Web.Models;

namespace InvoiceManagement.Web.Services;

//  MVC side service that calls the backend Auth API
public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public AuthService(
        HttpClient httpClient,
        IOptions<ApiSettings> apiSettings)
    {
        _httpClient = httpClient;
        _baseUrl = apiSettings.Value.BaseUrl;
    }

    // Calls the login endpoint and returns the response
    public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto request)
    {
        var json = JsonSerializer.Serialize(request);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync(
            $"{_baseUrl}api/auth/login",
            content);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<AuthResponseDto>(
            responseContent,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
    }

    // Calls the register endpoint to create a new user
    public async Task<bool> RegisterAsync(RegisterRequestDto request)
    {
        var json = JsonSerializer.Serialize(request);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync(
            $"{_baseUrl}api/auth/register",
            content);

        return response.IsSuccessStatusCode;
    }
}
