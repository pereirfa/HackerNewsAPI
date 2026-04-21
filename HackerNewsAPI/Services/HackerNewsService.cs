using HackerNewsApi.Models;
using HackerNewsAPI.Models;
using System.Text.Json;

namespace HackerNewsApi.Services
{
    public class HackerNewsService
    {
        private readonly HttpClient _httpClient;

        public HackerNewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public virtual async Task<List<Story>> GetBestStoriesAsync(int n)
        {
            var idsResponse = await _httpClient.GetStringAsync("https://hacker-news.firebaseio.com/v0/beststories.json");
            var ids = JsonSerializer.Deserialize<List<int>>(idsResponse);

            var tasks = ids.Take(n).Select(async id =>
            {
                var storyResponse = await _httpClient.GetStringAsync($"https://hacker-news.firebaseio.com/v0/item/{id}.json");
                var storyJson = JsonDocument.Parse(storyResponse).RootElement;

                return new Story
                {
                    Title = storyJson.GetProperty("title").GetString(),
                    Uri = storyJson.TryGetProperty("url", out var url) ? url.GetString() : null,
                    PostedBy = storyJson.GetProperty("by").GetString(),
                    Time = DateTimeOffset.FromUnixTimeSeconds(storyJson.GetProperty("time").GetInt64()).UtcDateTime,
                    Score = storyJson.GetProperty("score").GetInt32(),
                    CommentCount = storyJson.GetProperty("descendants").GetInt32()
                };
            });

            var stories = await Task.WhenAll(tasks);
            return stories.OrderByDescending(s => s.Score).ToList();
        }

        public virtual async Task<Item> GetItemAsync(int id)
        {
            var response = await _httpClient.GetStringAsync(
                $"https://hacker-news.firebaseio.com/v0/item/{id}.json?print=pretty"
            );

            var item = JsonSerializer.Deserialize<Item>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return item;
        }

        public virtual async Task<User> GetUserAsync(string id)
        {
            var response = await _httpClient.GetStringAsync(
                $"https://hacker-news.firebaseio.com/v0/user/{id}.json?print=pretty"
            );

            var user = JsonSerializer.Deserialize<User>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return user;
        }

        public virtual async Task<int> GetMaxItemAsync()
        {
            var response = await _httpClient.GetStringAsync(
                "https://hacker-news.firebaseio.com/v0/maxitem.json"
            );

            return JsonSerializer.Deserialize<int>(response);
        }

        public virtual async Task<List<int>> GetTopStoriesAsync()
        {
            var response = await _httpClient.GetStringAsync(
                "https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty"
            );
            return JsonSerializer.Deserialize<List<int>>(response);
        }

        public virtual async Task<List<int>> GetNewStoriesAsync()
        {
            var response = await _httpClient.GetStringAsync(
                "https://hacker-news.firebaseio.com/v0/newstories.json?print=pretty"
            );
            return JsonSerializer.Deserialize<List<int>>(response);
        }

        public virtual async Task<List<int>> GetBestStoriesAsync()
        {
            var response = await _httpClient.GetStringAsync(
                "https://hacker-news.firebaseio.com/v0/beststories.json?print=pretty"
            );
            return JsonSerializer.Deserialize<List<int>>(response);
        }

        public virtual async Task<List<int>> GetAskStoriesAsync()
        {
            var response = await _httpClient.GetStringAsync(
                "https://hacker-news.firebaseio.com/v0/askstories.json?print=pretty"
            );
            return JsonSerializer.Deserialize<List<int>>(response);
        }

        public virtual async Task<List<int>> GetShowStoriesAsync()
        {
            var response = await _httpClient.GetStringAsync(
                "https://hacker-news.firebaseio.com/v0/showstories.json?print=pretty"
            );
            return JsonSerializer.Deserialize<List<int>>(response);
        }

        public virtual async Task<List<int>> GetJobStoriesAsync()
        {
            var response = await _httpClient.GetStringAsync(
                "https://hacker-news.firebaseio.com/v0/jobstories.json?print=pretty"
            );
            return JsonSerializer.Deserialize<List<int>>(response);
        }

        public virtual async Task<Updates> GetUpdatesAsync()
        {
            var response = await _httpClient.GetStringAsync(
                "https://hacker-news.firebaseio.com/v0/updates.json"
            );

            var updates = JsonSerializer.Deserialize<Updates>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return updates;
        }
    }
}
