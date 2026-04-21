using HackerNewsApi.Models;
using HackerNewsApi.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNewsApi.Tests
{
    public class FakeHackerNewsService : HackerNewsService
    {
        public FakeHackerNewsService() : base(new HttpClient()) { }

        public override Task<Item> GetItemAsync(int id)
        {
            return Task.FromResult(new Item
            {
                Id = id,
                By = "fakeuser",
                Title = "Fake Item",
                Time = 1609459200,
                Score = 99
            });
        }

        public override Task<User> GetUserAsync(string id)
        {
            return Task.FromResult(new User
            {
                Id = id,
                Created = 1173923446,
                Karma = 1234,
                About = "Fake user for testing",
                Submitted = new List<int> { 1, 2, 3 }
            });
        }

        public override Task<int> GetMaxItemAsync()
        {
            return Task.FromResult(999999);
        }

        public override Task<List<int>> GetTopStoriesAsync()
        {
            return Task.FromResult(new List<int> { 101, 102, 103 });
        }

        public override Task<List<int>> GetNewStoriesAsync()
        {
            return Task.FromResult(new List<int> { 201, 202, 203 });
        }

        public override Task<List<int>> GetBestStoriesAsync()
        {
            return Task.FromResult(new List<int> { 301, 302, 303 });
        }

        public override Task<List<int>> GetAskStoriesAsync()
        {
            return Task.FromResult(new List<int> { 401, 402, 403 });
        }

        public override Task<List<int>> GetShowStoriesAsync()
        {
            return Task.FromResult(new List<int> { 501, 502, 503 });
        }

        public override Task<List<int>> GetJobStoriesAsync()
        {
            return Task.FromResult(new List<int> { 601, 602, 603 });
        }

        public override Task<Updates> GetUpdatesAsync()
        {
            return Task.FromResult(new Updates
            {
                Items = new List<int> { 701, 702 },
                Profiles = new List<string> { "fakeuser1", "fakeuser2" }
            });
        }
    }
}
