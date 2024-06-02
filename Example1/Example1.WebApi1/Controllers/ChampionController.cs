using Microsoft.AspNetCore.Mvc;
using ConsoleApp1;

namespace Example1.WebApi1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChampionController : ControllerBase
    {

        private readonly List<Item> Items = new List<Item> { new BucketHelmet(), new AmuletOfLost(), new Shilterica() };
        private static List<Champion> Champions = new List<Champion> ();

        private readonly ILogger<ChampionController> _logger;

        public ChampionController(ILogger<ChampionController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Get Champion")]
        public List<Champion> Get() {
            return Champions;
        }

        [HttpPost(Name = "Crate Champion")]
        public Champion Create([FromBody]Champion champion)
        {
            Champions.Add(champion);
            _logger.LogInformation($"Created champion with name {champion.Name}");
            return champion;
        }

        [HttpPut("{name}", Name = "Update Champion")]
        public Champion Update(string name,[FromBody] Champion champion)
        {
            
            return champion;
        }

        [HttpDelete("{name}", Name = "Delete Champion")]
        public IActionResult Delete(string name)
        {
            
            Champion championToRemove = Champions.FirstOrDefault(x => x.Name == name);
            
            return NoContent();
        }

    }
}