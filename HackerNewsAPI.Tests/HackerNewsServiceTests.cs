using HackerNewsApi.Models;
using HackerNewsApi.Services;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System.Text.Json;
using System;

namespace HackerNewsApi.Tests
{
    public class HackerNewsServiceTests
    {
        private HttpClient CreateMockHttpClient(string responseContent)
        {
            var handler = new MockHttpMessageHandler(responseContent);
            return new HttpClient(handler);
        }

        [Fact]
        public async Task GetItemAsync_ReturnsValidItem()
        {
            // Arrange: JSON simulado de um item
            var fakeJson = JsonSerializer.Serialize(new Item
            {
                Id = 123,
                By = "testuser",
                Title = "Test Story",
                Time = 1609459200, // 2021-01-01
                Score = 100
            });

            var httpClient = CreateMockHttpClient(fakeJson);
            var service = new HackerNewsService(httpClient);

            // Act
            var item = await service.GetItemAsync(123);

            // Assert
            Assert.NotNull(item);
            Assert.Equal(123, item.Id);
            Assert.Equal("testuser", item.By);
            Assert.Equal("Test Story", item.Title);
            Assert.Equal(100, item.Score);
            Assert.Equal(new DateTime(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc), item.TimeAsDate);
        }

        [Fact]
        public async Task GetUserAsync_ReturnsValidUser()
        {
            var fakeJson = JsonSerializer.Serialize(new User
            {
                Id = "fabio",
                Created = 1173923446,
                Karma = 2000,
                About = "Test user"
            });

            var httpClient = CreateMockHttpClient(fakeJson);
            var service = new HackerNewsService(httpClient);

            var user = await service.GetUserAsync("fabio");

            Assert.NotNull(user);
            Assert.Equal("fabio", user.Id);
            Assert.Equal(2000, user.Karma);
            Assert.Equal("Test user", user.About);
        }

        [Fact]
        public async Task GetMaxItemAsync_ReturnsValidId()
        {
            var fakeJson = "999999";
            var httpClient = CreateMockHttpClient(fakeJson);
            var service = new HackerNewsService(httpClient);

            var maxItemId = await service.GetMaxItemAsync();

            Assert.Equal(999999, maxItemId);
        }
    }

    // Mock HttpMessageHandler para simular respostas do HttpClient
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly string _responseContent;

        public MockHttpMessageHandler(string responseContent)
        {
            _responseContent = responseContent;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(_responseContent)
            };
            return Task.FromResult(response);
        }
    }
}
