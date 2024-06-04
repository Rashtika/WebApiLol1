using Microsoft.AspNetCore.Mvc;
using ConsoleApp1;
using Npgsql;
using System.Data.Common;

namespace Example1.WebApi1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChampionController : ControllerBase
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=Dakovo123;Database=ChampionDB;";

        private static List<Champion> Champions = new List<Champion> ();


        private readonly ILogger<ChampionController> _logger;
        public ChampionController(ILogger<ChampionController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "Insert Champion")]
        public IActionResult Insert([FromBody] Champion champion)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);

            string commandText = "Insert into \"Champion\" values (@Id, @Name, @Items)";
            var ChampionFromDb = new Champion();
            var command = new NpgsqlCommand(commandText, connection);

            connection.Open();

            Champions.Add(champion);

            return Ok(champion);
        }

        [HttpGet(Name = "Find Champions")]
        public List<Champion> GetAll()
        {
            List<Champion> champions = new List<Champion>();
            using var connection = new NpgsqlConnection(connectionString);
            {
                connection.Open();
<<<<<<< HEAD
                string sql = "SELECT * FROM \"Champion\" c LEFT JOIN \"Weapons\" w ON c.\"Id\"=w.\"Id\" ";
=======
                string sql = "SELECT * FROM \"Champion\"";
>>>>>>> 8772c826fd28bea845e2bbd07737bdc39f4fa5d9
                using NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);

                //SqlCommand command = new SqlCommand();

                using var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
<<<<<<< HEAD
                    while (reader.Read())
                    {
                        champions.Add(new Champion
                        {
                            Name = reader["Name"].ToString(),
                            Id = Guid.Parse(reader["Id"].ToString())

                        });
=======
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
>>>>>>> 8772c826fd28bea845e2bbd07737bdc39f4fa5d9
                }
            }
            connection.Close();
            return Champions;
        }
<<<<<<< HEAD
        
        /*[HttpGet(Name = "Get Champions")]
=======

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
>>>>>>> 8772c826fd28bea845e2bbd07737bdc39f4fa5d9
        public List<Champion> Get() {
            return Champions;
        }

        [HttpPost(Name = "Crate Champion")]
        public Champion Create([FromBody]Champion champion)
        {
            Champions.Add(champion);
            _logger.LogInformation($"Created champion with name {champion.Name}");
            return champion;
<<<<<<< HEAD
        }*/
=======
        }
        */
>>>>>>> 8772c826fd28bea845e2bbd07737bdc39f4fa5d9

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