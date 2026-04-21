using HackerNewsApi.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace HackerNewsApi.Tests.Controllers;

public class ItemControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ItemControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Remove qualquer registro existente de HackerNewsService
                services.RemoveAll<HackerNewsService>();
                services.RemoveAll<CachedHackerNewsService>();

                // Registra o fake no lugar do real
                services.AddSingleton<CachedHackerNewsService, FakeCachedHackerNewsService>();
            });
        }).CreateClient();

    }

    [Fact]
    public async Task GetItem_ReturnsFakeItem()
    {
        var response = await _client.GetAsync("/v0/item/123");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        Assert.Contains("\"id\":123", json);
        Assert.Contains("\"title\":\"Fake Item\"", json);
    }
}
