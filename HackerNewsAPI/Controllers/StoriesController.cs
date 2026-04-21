using HackerNewsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsApi.Controllers
{
    [ApiController]
    [Route("v0/[controller]")]
    public class StoriesController : ControllerBase
    {
        private readonly CachedHackerNewsService _service;

        public StoriesController(CachedHackerNewsService service)
        {
            _service = service;
        }

        [HttpGet("topstories")]
        public async Task<IActionResult> GetTopStories()
        {
            var stories = await _service.GetTopStoriesAsync();
            return Ok(stories);
        }

        [HttpGet("newstories")]
        public async Task<IActionResult> GetNewStories()
        {
            var stories = await _service.GetNewStoriesAsync();
            return Ok(stories);
        }

        [HttpGet("beststories")]
        public async Task<IActionResult> GetBestStories()
        {
            var stories = await _service.GetBestStoriesAsync();
            return Ok(stories);
        }

        [HttpGet("askstories")]
        public async Task<IActionResult> GetAskStories()
        {
            var stories = await _service.GetAskStoriesAsync();
            return Ok(stories);
        }

        [HttpGet("showstories")]
        public async Task<IActionResult> GetShowStories()
        {
            var stories = await _service.GetShowStoriesAsync();
            return Ok(stories);
        }

        [HttpGet("jobstories")]
        public async Task<IActionResult> GetJobStories()
        {
            var stories = await _service.GetJobStoriesAsync();
            return Ok(stories);
        }
    }
}
