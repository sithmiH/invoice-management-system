using InvoiceManagement.Web.DTOs.Auth;
using InvoiceManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagement.Web.Controllers;

public class AccountController : Controller
{
    private readonly AuthService _authService;

    public AccountController(AuthService authService)
    {
        _authService = authService;
    }

    // Display Login page
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // Handle Login
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        var response = await _authService.LoginAsync(request);

        if (response == null)
        {
            ViewBag.Error = "Invalid email or password.";
            return View(request);
        }

        HttpContext.Session.SetString("Token", response.Token);
        HttpContext.Session.SetString("Name", response.Name);
        HttpContext.Session.SetString("Email", response.Email);
        HttpContext.Session.SetString("Role", response.Role);

        return RedirectToAction("Index", "Dashboard");
    }

    // Display Register page
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // Handle Register
    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        var success = await _authService.RegisterAsync(request);

        if (!success)
        {
            ViewBag.Error = "Registration failed.";
            return View(request);
        }

        TempData["Success"] = "Registration successful. Please login.";

        return RedirectToAction(nameof(Login));
    }

    // Logout
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return RedirectToAction(nameof(Login));
    }
}
