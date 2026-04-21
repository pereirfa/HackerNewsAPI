using HackerNewsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsApi.Controllers
{
    [ApiController]
    [Route("v0/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly CachedHackerNewsService _service;

        public ItemController(CachedHackerNewsService service)
        {
            _service = service;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _service.GetItemAsync(id);
            if (item == null) return NotFound();

            return Ok(item);
        }
    }
}
