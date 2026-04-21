using HackerNewsApi.Services;
using HackerNewsApi.Tests;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;


namespace HackerNewsApi.Tests.Controllers;

public class StoriesControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public StoriesControllerTests(WebApplicationFactory<Program> factory)
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

    [Theory]
    [InlineData("/v0/stories/topstories", "101")]
    [InlineData("/v0/stories/newstories", "201")]
    [InlineData("/v0/stories/beststories", "301")]
    [InlineData("/v0/stories/askstories", "401")]
    [InlineData("/v0/stories/showstories", "501")]
    [InlineData("/v0/stories/jobstories", "601")]
    public async Task StoriesEndpoints_ReturnFakeIds(string url, string expectedContent)
    {
        var response = await _client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        Assert.Contains(expectedContent, json);
    }
}
