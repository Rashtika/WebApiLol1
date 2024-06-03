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
        private readonly object _context;

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
        public async Task<IActionResult> Update(string name,[FromBody] Champion champion)
        {

            bool ChampionExist = Champions.Exists(obj => obj.Name == name);

            if (!ChampionExist)
            {
                return BadRequest();
            }

            Champion GetChampionByName = Champions.FirstOrDefault(champion => champion.Name == name);

            if (GetChampionByName != null)
            {
                GetChampionByName.Name = champion.Name;
            }

            return NoContent();


        }


        [HttpDelete("{name}", Name = "Delete Champion")]
        public IActionResult Delete(string name)
        {
            bool ChampionExist = Champions.Exists(obj => obj.Name == name);

            if (!ChampionExist)
            {
                return BadRequest();
            }

            Champion championToRemove = Champions.FirstOrDefault(x => x.Name == name);

            if(championToRemove != null)
            {
                Champions.Remove(championToRemove);
            }


            
            return NoContent();
        }

    }
}