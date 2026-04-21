using HackerNewsApi.Models;
using HackerNewsAPI.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace HackerNewsApi.Services
{
    public class CachedHackerNewsService
    {
        private readonly HackerNewsService _service;
        private readonly IMemoryCache _cache;

        public CachedHackerNewsService(HackerNewsService service, IMemoryCache cache)
        {
            _service = service;
            _cache = cache;
        }

        // Stories com detalhes completos
        public virtual async Task<List<Story>> GetBestStoriesAsync(int n)
        {
            return await _cache.GetOrCreateAsync($"beststories_{n}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
                return await _service.GetBestStoriesAsync(n);
            });
        }

        // Item individual
        public virtual async Task<Item> GetItemAsync(int id)
        {
            return await _cache.GetOrCreateAsync($"item_{id}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return await _service.GetItemAsync(id);
            });
        }

        // Usuário
        public virtual async Task<User> GetUserAsync(string id)
        {
            return await _cache.GetOrCreateAsync($"user_{id}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return await _service.GetUserAsync(id);
            });
        }

        // Max Item ID
        public virtual async Task<int> GetMaxItemAsync()
        {
            return await _cache.GetOrCreateAsync("maxitem", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                return await _service.GetMaxItemAsync();
            });
        }

        // Top Stories
        public virtual async Task<List<int>> GetTopStoriesAsync()
        {
            return await _cache.GetOrCreateAsync("topstories", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
                return await _service.GetTopStoriesAsync();
            });
        }

        // New Stories
        public virtual async Task<List<int>> GetNewStoriesAsync()
        {
            return await _cache.GetOrCreateAsync("newstories", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
                return await _service.GetNewStoriesAsync();
            });
        }

        // Best Stories (IDs)
        public virtual async Task<List<int>> GetBestStoriesAsync()
        {
            return await _cache.GetOrCreateAsync("beststories_ids", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
                return await _service.GetBestStoriesAsync();
            });
        }

        // Ask Stories
        public virtual async Task<List<int>> GetAskStoriesAsync()
        {
            return await _cache.GetOrCreateAsync("askstories", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
                return await _service.GetAskStoriesAsync();
            });
        }

        // Show Stories
        public virtual async Task<List<int>> GetShowStoriesAsync()
        {
            return await _cache.GetOrCreateAsync("showstories", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
                return await _service.GetShowStoriesAsync();
            });
        }

        // Job Stories
        public virtual async Task<List<int>> GetJobStoriesAsync()
        {
            return await _cache.GetOrCreateAsync("jobstories", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);
                return await _service.GetJobStoriesAsync();
            });
        }

        // Updates
        public virtual async Task<Updates> GetUpdatesAsync()
        {
            return await _cache.GetOrCreateAsync("updates", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                return await _service.GetUpdatesAsync();
            });
        }
    }
}
