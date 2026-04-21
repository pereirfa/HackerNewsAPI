using HackerNewsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsApi.Controllers
{
    [ApiController]
    [Route("v0/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly CachedHackerNewsService _service;

        public UserController(CachedHackerNewsService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _service.GetUserAsync(id);
            if (user == null) return NotFound();

            return Ok(user);
        }
    }
}
