using HackerNewsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsApi.Controllers
{
    [ApiController]
    [Route("v0/[controller]")]
    public class MaxItemController : ControllerBase
    {
        private readonly CachedHackerNewsService _service;

        public MaxItemController(CachedHackerNewsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxItem()
        {
            var maxItemId = await _service.GetMaxItemAsync();
            return Ok(new { maxItemId });
        }
    }
}
