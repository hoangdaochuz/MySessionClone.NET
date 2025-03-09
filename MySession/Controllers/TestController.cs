
using Microsoft.AspNetCore.Mvc;
using MySession.MySession;

namespace MySession.Controllers;

public class TestController:Controller
{
    private readonly ILogger<TestController> _logger;
    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }
    public IActionResult TestGetSession()
    {
        var session = HttpContext.GetSession();
        session.SetString("Name", "123456789ABC");
        
        // session = HttpContext.GetSession();
        string? name = session.GetString("Name");
        _logger.LogInformation($"Name: {name}");
        if (name == "123456789ABC")
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    public async Task<IActionResult> GetSessionValue(string key)
    {
        var session = HttpContext.GetSession();
        await session.LoadAsync();
        string? value = session.GetString(key);
        return Ok(value);
    }

    public async Task<IActionResult> SetSessionValue(string key,string value)
    {
        var session = HttpContext.GetSession();
        await session.LoadAsync(); // --> fill value into _store of session
        session.SetString(key, value);
        await session.CommitAsync();
        return Ok();
    }
    
    
    
    
}