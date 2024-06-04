using Microsoft.AspNetCore.Mvc;
using ConsoleApp1;
using Npgsql;

namespace Example1.WebApi1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class ChampionController : ControllerBase
    {

        string connectionString = "Host=localhost;Port=5433;Username=postgres;Password=Dakovo123;Database=ChampionDB;";

        private readonly List<Item> Items = new List<Item> { new BucketHelmet(), new AmuletOfLost(), new Shilterica() };
        private static List<Champion> Champions = new List<Champion> ();


        private readonly ILogger<ChampionController> _logger;
        public ChampionController(ILogger<ChampionController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Find Champions")]
        public List<Champion> GetAll()
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            {
                connection.Open();
                string sql = "SELECT * FROM \"Champion\"";
                using NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);

                //SqlCommand command = new SqlCommand();

                using var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    Champion champion = new Champion
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        InventoryId = reader.GetGuid(reader.GetOrdinal("InventoryId")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                        DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                        CreatedByUserId = reader.GetInt32(reader.GetOrdinal("CreatedByUserId")),
                        UpdatedByUserId = reader.GetInt32(reader.GetOrdinal("UpdatedByUserId"))
                    };
                }
            }
            connection.Close();
            return Champions;
        }

        [HttpPost(Name = "Insert Champion")]
        public Champion Insert([FromBody]Champion champion)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            Champions.Add(champion);

            return champion;
        }

        /*
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
        */

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
        [HttpGet("test-connection", Name = "TestDatabaseConnection")]
        public IActionResult TestConnection()
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                connection.Open();
                return Ok("Connection to the database is successful.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to connect to the database.");
                return StatusCode(500, "Failed to connect to the database.");
            }
        }

    }
}