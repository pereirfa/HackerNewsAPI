using HackerNewsApi.Models;
using HackerNewsApi.Services;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System.Text.Json;

namespace HackerNewsApi.Tests
{
    public class CachedHackerNewsServiceTests
    {
        [Fact]
        public async Task GetItemAsync_UsesCacheOnSecondCall()
        {
            // Arrange: JSON simulado de um item
            var fakeJson = JsonSerializer.Serialize(new Item
            {
                Id = 123,
                By = "testuser",
                Title = "Test Story",
                Time = 1609459200,
                Score = 100
            });

            var handler = new CountingHttpMessageHandler(fakeJson);
            var httpClient = new HttpClient(handler);
            var service = new HackerNewsService(httpClient);
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var cachedService = new CachedHackerNewsService(service, memoryCache);

            // Act: primeira chamada (vai bater no HttpClient)
            var item1 = await cachedService.GetItemAsync(123);

            // Act: segunda chamada (deve vir do cache)
            var item2 = await cachedService.GetItemAsync(123);

            // Assert
            Assert.Equal(item1.Id, item2.Id);
            Assert.Equal(1, handler.CallCount); // HttpClient chamado apenas uma vez
        }
    }

    // Handler que conta quantas vezes o HttpClient foi chamado
    public class CountingHttpMessageHandler : HttpMessageHandler
    {
        private readonly string _responseContent;
        public int CallCount { get; private set; } = 0;

        public CountingHttpMessageHandler(string responseContent)
        {
            _responseContent = responseContent;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            CallCount++;
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(_responseContent)
            };
            return Task.FromResult(response);
        }
    }
}
