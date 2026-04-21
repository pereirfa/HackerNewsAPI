using HackerNewsApi.Services;
using HackerNewsApi.Tests;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace HackerNewsApi.Tests.Controllers;

public class UserControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UserControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<HackerNewsService>();
                services.RemoveAll<CachedHackerNewsService>();
                services.AddSingleton<CachedHackerNewsService, FakeCachedHackerNewsService>();
            });
        }).CreateClient();
    }

    [Fact]
    public async Task GetUser_ReturnsFakeUser()
    {
        var response = await _client.GetAsync("/v0/user/fakeuser");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        Assert.Contains("\"id\":\"fakeuser\"", json);
        Assert.Contains("\"karma\":1234", json);
    }
}
