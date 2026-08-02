using InvoiceManagement.Web.DTOs.Auth;
using InvoiceManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagement.Web.Controllers;

public class AccountController : Controller
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    // Display Login page
    [HttpGet]
    public IActionResult Login()
    {
        if (HttpContext.Session.GetString("Token") != null)
        {
            return RedirectToAction("Index", "Dashboard");
        }

        return View();
    }

    // Handle Login operation
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
            TempData["Error"] = "Invalid email or password.";
            return View(request);
        }

        HttpContext.Session.SetString("Token", response.Token);
        HttpContext.Session.SetString("Name", response.Name);
        HttpContext.Session.SetString("Email", response.Email);
        HttpContext.Session.SetString("Role", response.Role);

        TempData["Success"] = "Login successful.";

        return RedirectToAction("Index", "Dashboard");
    }

    // Display Register page
    [HttpGet]
    public IActionResult Register()
    {
        if (HttpContext.Session.GetString("Token") != null)
        {
            return RedirectToAction("Index", "Dashboard");
        }

        return View(new RegisterRequestDto());
    }

    // Handle Register operation
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
