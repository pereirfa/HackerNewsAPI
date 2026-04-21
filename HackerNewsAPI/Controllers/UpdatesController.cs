using HackerNewsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsApi.Controllers
{
    [ApiController]
    [Route("v0/[controller]")]
    public class UpdatesController : ControllerBase
    {
        private readonly CachedHackerNewsService _service;

        public UpdatesController(CachedHackerNewsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetUpdates()
        {
            var updates = await _service.GetUpdatesAsync();
            return Ok(updates);
        }
    }
}

