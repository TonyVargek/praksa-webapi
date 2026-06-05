using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Text;

namespace Example.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        string CONNECTION_STRING = "Host=localhost;Port=5432;Database=testdb2;Username=postgres;Password=postgres";

        [HttpGet("getAll")]
        public IActionResult getAll([FromQuery] string typeMeal = "", string brand = "" )
        {
            try
            {
                List<Food> foods = new List<Food>();
                using NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);

                StringBuilder sb = new StringBuilder();

                sb.Append("SELECT * FROM \"Food\" WHERE 1 = 1 ");
                using NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;

                if (typeMeal != "")
                {
                    sb.Append("AND \"TypeMeal\" = @typemeal");
                    command.Parameters.AddWithValue("@typemeal", typeMeal);
                }
                if (brand != "")
                {
                    sb.Append("AND \"Brand\" = @brand");
                    command.Parameters.AddWithValue("@brand", brand);
                }

                command.CommandText = sb.ToString();

                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Food food = new Food();
                        food.Id = Convert.ToInt32(reader["Id"]);
                        food.Name = reader["Name"].ToString();
                        food.Description = reader["Description"].ToString();
                        food.TypeMeal = reader["TypeMeal"].ToString();
                        food.Brand = reader["Brand"].ToString();
                        foods.Add(food);
                    }
                }

                connection.Close();

                if (foods.Count() > 0)
                {
                    return Ok(foods);
                }
                return BadRequest("");
            }
            catch (Exception e)
            {
                return BadRequest("");
            }
        }
  
        [HttpGet("{id}")]
        public IActionResult FindFood(int id)
        {
            try
            {
                Food food = new Food();
                using NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);

                string comm = "SELECT * FROM \"Food\" WHERE \"Id\" = @id";
                using NpgsqlCommand command = new NpgsqlCommand(comm);
                command.Connection = connection;
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                NpgsqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    reader.Read();
                    food.Id = Convert.ToInt32(reader["Id"]);
                    food.Name = reader["Name"].ToString();
                    food.Description = reader["Description"].ToString();
                    food.TypeMeal = reader["TypeMeal"].ToString();
                    food.Brand = reader["Brand"].ToString();
                }

                connection.Close();

                if (food.Name != null)
                {
                    return Ok(food);
                }

                return BadRequest("");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult AddFood(Food food)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);

                string comm = "INSERT INTO \"Food\" (\"Name\", \"Description\", \"TypeMeal\", \"Brand\") VALUES (@name, @description, @typemeal, @brand)";
                using NpgsqlCommand command = new NpgsqlCommand(comm);
                command.Connection = connection;

                command.Parameters.AddWithValue("@name", food.Name);
                command.Parameters.AddWithValue("@description", food.Description);
                command.Parameters.AddWithValue("@typemeal", food.TypeMeal);
                command.Parameters.AddWithValue("@brand", food.Brand);


                connection.Open();

                var rowsAffected = command.ExecuteNonQuery();

                connection.Close();

                if (rowsAffected > 0)
                {
                    return Ok("Succesfully added");
                }
                return BadRequest("");


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFood(int id, Food newFood)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);

                string comm = "UPDATE \"Food\" SET (\"Name\", \"Description\", \"TypeMeal\", \"Brand\") = (@name, @description, @typemeal, @brand) WHERE \"Id\" = @id";
                using NpgsqlCommand command = new NpgsqlCommand(comm, connection);

                command.Parameters.AddWithValue("@name", newFood.Name);
                command.Parameters.AddWithValue("@description", newFood.Description);
                command.Parameters.AddWithValue("@typemeal", newFood.TypeMeal);
                command.Parameters.AddWithValue("@brand", newFood.Brand);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                int affectedRows = command.ExecuteNonQuery();

                connection.Close();

                if(affectedRows > 0)
                {
                    return Ok("Updated sucesfully");
                }

                return BadRequest("");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveFood(int id)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);

                string comm = "DELETE FROM \"Food\" WHERE \"Id\" = @id";
                using NpgsqlCommand command = new NpgsqlCommand(comm, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                int affectedRows = command.ExecuteNonQuery();

                connection.Close();

                if (affectedRows > 0)
                {
                    return Ok("Deleted sucesfully");
                }

                return BadRequest("");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
