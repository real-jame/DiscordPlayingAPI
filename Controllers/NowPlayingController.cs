using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DiscordPlayingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NowPlayingController : ControllerBase
    {
        private readonly DiscordBotService _discordBotService;

        public NowPlayingController(DiscordBotService discordBotService)
        {
            _discordBotService = discordBotService;
        }

        // GET: api/<NowPlayingController>
        [HttpGet]
        public async Task<ContentResult> Get()
        {
            var activity = await _discordBotService.GetStatusAsync();
            Console.WriteLine(activity.Details);
            return base.Content($"<div>{activity.Details}</div>", "text/html");
        }

        // GET api/<NowPlayingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "hi";
        }

        // POST api/<NowPlayingController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<NowPlayingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NowPlayingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
