using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MySessionIntegrationTest;

public class MySessionTest:IClassFixture<WebApplicationFactory<MySession.Program>>
{
    private readonly HttpClient _factory;

    public MySessionTest(WebApplicationFactory<MySession.Program> factory)
    {
        _factory = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task Call_TestGetSession_Return_OK_Async()
    {
        var response = await _factory.GetAsync("/Test/TestGetSession");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Call_TestSetAndGetValueSession_Return_OK_Async()
    {
        string radomValue = Guid.NewGuid().ToString();
        await _factory.GetAsync($"Test/SetSessionValue?key=Test_key&value={radomValue}");

        var response = await _factory.GetAsync($"/Test/GetSessionValue?key=Test_key");
        string responseString = await response.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
        Assert.Equal(responseString,radomValue);
    }
}