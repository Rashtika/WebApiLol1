using Microsoft.AspNetCore.Mvc;
using ConsoleApp1;
using Npgsql;

namespace Example1.WebApi1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class ChampionController : ControllerBase
    {

        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=Dakovo123;Database=ChampionDB;";

        private static List<Champion> Champions = new List<Champion>();

        private readonly ILogger<ChampionController> _logger;
        public ChampionController(ILogger<ChampionController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Find Champions")]
        public IActionResult GetAll()
        {
            List<Champion> champions = new List<Champion>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM \"Champion\"";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
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
                                champions.Add(champion);
                            }
                        }
                        connection.Close();
                    }
                }
                return Ok(champions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpPost(Name = "Insert Champion")]
        public ActionResult<Champion> Insert([FromBody] Champion champion)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string inventorySql = "INSERT INTO \"Inventory\" (\"DateCreated\") VALUES (@DateCreated) RETURNING \"Id\"";
                Guid newInventoryId;
                using (NpgsqlCommand inventoryCmd = new NpgsqlCommand(inventorySql, connection))
                {
                    inventoryCmd.Parameters.AddWithValue("@DateCreated", DateTime.UtcNow);
                    newInventoryId = (Guid)inventoryCmd.ExecuteScalar();
                }

                string sql = "INSERT INTO \"Champion\" (\"Id\", \"Name\", \"InventoryId\", \"IsActive\", \"DateCreated\", \"CreatedByUserId\", \"UpdatedByUserId\") " +
                                 "VALUES (@Id, @Name, @InventoryId, @IsActive, @DateCreated, @CreatedByUserId, @UpdatedByUserId)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                {
                    champion.InventoryId = newInventoryId;
                    champion.DateCreated = DateTime.UtcNow;

                    cmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("@Name", champion.Name);
                    cmd.Parameters.AddWithValue("@InventoryId", champion.InventoryId);
                    cmd.Parameters.AddWithValue("@IsActive", champion.IsActive);
                    cmd.Parameters.AddWithValue("@DateCreated", champion.DateCreated);
                    cmd.Parameters.AddWithValue("@CreatedByUserId", champion.CreatedByUserId);
                    cmd.Parameters.AddWithValue("@UpdatedByUserId", champion.UpdatedByUserId);

                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            return CreatedAtAction(nameof(GetAll), new { id = champion.Id }, champion);
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

        [HttpPut("{id}", Name = "Update Champion")]
        public IActionResult Update(Guid id, [FromBody] Champion champion)
        {
            if (id != champion.Id)
            {
                return BadRequest("This champion does not exist.");
            }
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string checkSql = "SELECT COUNT(1) FROM \"Champion\" WHERE \"Id\" = @Id";
                using (NpgsqlCommand checkCmd = new NpgsqlCommand(checkSql, connection))
                {
                    checkCmd.Parameters.AddWithValue("@Id", id);
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count == 0)
                    {
                        return NotFound("Champion not found");
                    }
                }
                string sql = "UPDATE \"Champion\" SET \"Name\" = @Name, \"InventoryId\" = @InventoryId, \"IsActive\" = @IsActive, " +
                    "\"UpdatedByUserId\" = @UpdatedByUserId, \"DateUpdated\" = @DateUpdated WHERE \"Id\" = @Id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", champion.Id);
                    cmd.Parameters.AddWithValue("@Name", champion.Name);
                    cmd.Parameters.AddWithValue("@InventoryId", champion.InventoryId);
                    cmd.Parameters.AddWithValue("@IsActive", champion.IsActive);
                    cmd.Parameters.AddWithValue("@UpdatedByUserId", champion.UpdatedByUserId);
                    cmd.Parameters.AddWithValue("@DateUpdated", DateTime.UtcNow);

                    cmd.ExecuteNonQuery();
                }
                connection.Close();
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

            if (championToRemove != null)
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