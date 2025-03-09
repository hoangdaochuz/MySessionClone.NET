using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySession.Models;
using MySession.MySession;

namespace MySession.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var session = HttpContext.GetSession();
        session.SetString("name", "John");
        await session.CommitAsync();
        return View();
    }

    public IActionResult Privacy()
    {
        var session = HttpContext.GetSession();
        string? name = session.GetString("name");
        return View("Privacy", name);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}