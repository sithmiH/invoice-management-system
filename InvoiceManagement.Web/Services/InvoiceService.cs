using System.Net.Http.Headers;
using System.Text.Json;
using InvoiceManagement.Web.DTOs.Invoice;
using InvoiceManagement.Web.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using InvoiceManagement.Web.DTOs.User;

namespace InvoiceManagement.Web.Services;

// MVC side service that calls the backend Invoice and User endpoints
public class InvoiceService : IInvoiceService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _baseUrl;

    public InvoiceService(
        HttpClient httpClient,
        IHttpContextAccessor httpContextAccessor,
        IOptions<ApiSettings> apiSettings)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _baseUrl = apiSettings.Value.BaseUrl;
    }

    // Retrieves all invoices
    public async Task<List<InvoiceResponseDto>> GetAllInvoicesAsync()
    {
        var token = _httpContextAccessor.HttpContext?
            .Session.GetString("Token");

        if (string.IsNullOrEmpty(token))
        {
            return new List<InvoiceResponseDto>();
        }

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync($"{_baseUrl}api/invoice");

        if (!response.IsSuccessStatusCode)
        {
            return new List<InvoiceResponseDto>();
        }

        var json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<InvoiceResponseDto>>(
                   json,
                   new JsonSerializerOptions
                   {
                       PropertyNameCaseInsensitive = true
                   })
               ?? new List<InvoiceResponseDto>();
    }

    // Retrieves all users to populate the Create Invoice dropdown
    public async Task<List<UserResponseDto>> GetAllUsersAsync()
    {
        var token = _httpContextAccessor.HttpContext?
            .Session.GetString("Token");

        if (string.IsNullOrEmpty(token))
        {
            return new List<UserResponseDto>();
        }

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync($"{_baseUrl}api/users");

        if (!response.IsSuccessStatusCode)
        {
            return new List<UserResponseDto>();
        }

        var users = await response.Content.ReadFromJsonAsync<List<UserResponseDto>>();

        return users ?? new List<UserResponseDto>();
    }

    // Creates a new invoice
    public async Task<bool> CreateInvoiceAsync(CreateInvoiceDto request)
    {
        var token = _httpContextAccessor.HttpContext?
            .Session.GetString("Token");

        if (string.IsNullOrEmpty(token))
        {
            return false;
        }

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsJsonAsync(
            $"{_baseUrl}api/invoice",
            request);

        return response.IsSuccessStatusCode;
    }

    // Retrieves an invoice by ID
    public async Task<InvoiceResponseDto?> GetInvoiceByIdAsync(int id)
    {
        var token = _httpContextAccessor.HttpContext?
            .Session.GetString("Token");

        if (string.IsNullOrEmpty(token))
            return null;

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync($"{_baseUrl}api/invoice/{id}");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<InvoiceResponseDto>();
    }

    public async Task<bool> UpdateInvoiceAsync(int id, UpdateInvoiceDto request)
    {
        var token = _httpContextAccessor.HttpContext?
            .Session.GetString("Token");

        if (string.IsNullOrEmpty(token))
            return false;

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PutAsJsonAsync(
            $"{_baseUrl}api/invoice/{id}",
            request);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteInvoiceAsync(int id)
    {
        var token = _httpContextAccessor.HttpContext?
            .Session.GetString("Token");

        if (string.IsNullOrEmpty(token))
            return false;

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.DeleteAsync(
            $"{_baseUrl}api/invoice/{id}");

        return response.IsSuccessStatusCode;
    }
}
