using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Text;

namespace Example.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        string CONNECTION_STRING = "Host=localhost;Port=5432;Database=testdb2;Username=postgres;Password=postgres";

        [HttpGet("getAll")]
        public IActionResult GetAll(string? firstName = null, string? lastName = null, string? favoriteFood = null, float bmi = -1)
        {
            try
            {
                List<Member> members = new List<Member>();
                using NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);

                StringBuilder sb = new StringBuilder();

                sb.Append("SELECT m.*, f.\"Name\", f.\"Description\", f.\"TypeMeal\", f.\"Brand\" FROM \"Member\" as m LEFT JOIN \"Food\" as f on m.\"FoodId\" = f.\"Id\" WHERE 1 = 1 ");
                using NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;

                if (firstName != null)
                {
                    sb.Append(" AND m.\"FirstName\" = @firstname");
                    command.Parameters.AddWithValue("@firstname", firstName);
                }
                if (lastName != null)
                {
                    sb.Append(" AND m.\"LastName\" = @lastname");
                    command.Parameters.AddWithValue("@lastname", lastName);
                }
                if (bmi > 0)
                {
                    sb.Append(" AND m.\"BMI\" > @bmi");
                    command.Parameters.AddWithValue("@bmi", Convert.ToDecimal(bmi));
                }
                if (favoriteFood != null)
                {
                    sb.Append(" AND f.\"Name\" = @favoriteFood");
                    command.Parameters.AddWithValue("@favoriteFood", favoriteFood);
                }

                command.CommandText = sb.ToString();

                connection.Open();

                NpgsqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Member member = new Member();
                        member.Id = Convert.ToInt32(reader["Id"]);
                        member.FirstName = reader["FirstName"].ToString();
                        member.LastName = reader["LastName"].ToString();
                        member.Height = reader.IsDBNull(3) ? -1 : Convert.ToInt32(reader[3]);
                        member.Weight = reader.IsDBNull(4) ? -1 : Convert.ToInt32(reader[4]);
                        member.FoodId = reader.IsDBNull(6) ? -1 : Convert.ToInt32(reader[6]);
                        if(member.FoodId > 0)
                        {
                            Food food = new Food();
                            food.Id = member.FoodId;
                            food.Name = reader[7].ToString();
                            food.Description = reader[8].ToString();
                            food.TypeMeal = reader[9].ToString();
                            food.Brand = reader[9].ToString();
                            member.Food = food;
                        }
                        if(member.Id > 0)
                        {
                            members.Add(member);
                        }
                    }
                }

                connection.Close();

                if(members.Count() > 0)
                {
                    return Ok(members);
                }

                return BadRequest("");
            } 
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Member member = new Member();
                using NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);

                StringBuilder sb = new StringBuilder();

                string comm = "SELECT m.\"Id\", m.\"FirstName\", m.\"LastName\", m.\"Weight\", m.\"Height\"," +
                    " m.\"FoodId\", f.\"Name\", f.\"Description\", f.\"TypeMeal\", f.\"Brand\" " +
                    "FROM \"Member\" as m LEFT JOIN \"Food\" as f on m.\"FoodId\" = f.\"Id\" WHERE m.\"Id\" = @id";
                using NpgsqlCommand command = new NpgsqlCommand(comm);
                command.Connection = connection;

                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    member.Id = Convert.ToInt32(reader["Id"]);
                    member.FirstName = reader["FirstName"].ToString();
                    member.LastName = reader["LastName"].ToString();
                    member.Weight = Convert.ToInt32(reader["Weight"]);
                    member.Height = Convert.ToInt32(reader["Height"]);
                    member.FoodId = reader.IsDBNull(6) ? -1 : Convert.ToInt32(reader["FoodId"]);
                    if(member.FoodId > 0)
                    {
                        Food food = new Food();
                        food.Id = member.FoodId;
                        food.Name = reader["Name"].ToString();
                        food.Description = reader["Description"].ToString();
                        food.TypeMeal = reader["TypeMeal"].ToString();
                        food.Brand = reader["Brand"].ToString();
                        member.Food = food;
                    }
                }
                connection.Close();

                if(member.Id > 0)
                {
                    return Ok(member);
                }

                return BadRequest("");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(Member member)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);

                string comm = "INSERT INTO \"Member\" (\"FirstName\", \"LastName\", \"Weight\", \"Height\", \"BMI\") VALUES (@firstname, @lastname, @weight, @height, @bmi)";
                using NpgsqlCommand command = new NpgsqlCommand(comm);
                command.Connection = connection;

                command.Parameters.AddWithValue("@firstname", member.FirstName);
                command.Parameters.AddWithValue("@lastname", member.LastName);
                command.Parameters.AddWithValue("@weight", member.Weight);
                command.Parameters.AddWithValue("@height", member.Height);
                command.Parameters.AddWithValue("@bmi", member.BMI);

                connection.Open();

                int affectedRows = command.ExecuteNonQuery();

                connection.Close();

                if(affectedRows > 0)
                {
                    return NoContent();
                }
                return BadRequest("");

            } 
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Member newMember)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);

                string comm = "UPDATE \"Member\" SET (\"FirstName\", \"LastName\", \"Weight\", \"Height\", \"BMI\", \"FoodId\") = (@firstname, @lastname, @weight, @height, @bmi, @foodid) WHERE \"Id\" = @id";
                using NpgsqlCommand command = new NpgsqlCommand(comm);
                command.Connection = connection;

                command.Parameters.AddWithValue("@firstname", newMember.FirstName);
                command.Parameters.AddWithValue("@lastname", newMember.LastName);
                command.Parameters.AddWithValue("@weight", newMember.Weight);
                command.Parameters.AddWithValue("@height", newMember.Height);
                command.Parameters.AddWithValue("@bmi", newMember.BMI);
                command.Parameters.AddWithValue("@foodid", newMember.FoodId);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                int affectedRows = command.ExecuteNonQuery();

                connection.Close();

                if (affectedRows > 0)
                {
                    return NoContent();
                }
                return BadRequest("");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);

                string comm = "DELETE FROM \"Member\" WHERE \"Id\" = @id";
                using NpgsqlCommand command = new NpgsqlCommand(comm);
                command.Connection = connection;

                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                int affectedRows = command.ExecuteNonQuery();

                connection.Close();

                if (affectedRows > 0)
                {
                    return NoContent();
                }
                return BadRequest("");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getfoodbybmi")]
        public IActionResult GetFoodByBMI(float bmi)
        {
            try
            {
                List<Food> foods = new List<Food>();
                using NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);

                string comm = "SELECT f.* FROM \"Member\" as m INNER JOIN \"Food\" as f on m.\"FoodId\" = f.\"Id\" WHERE m.\"BMI\" > @bmi";
                using NpgsqlCommand command = new NpgsqlCommand(comm);
                command.Connection = connection;

                command.Parameters.AddWithValue("@bmi", bmi);

                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Food food = new Food();
                        food.Id = Convert.ToInt32(reader[0]);
                        food.Name = reader[1].ToString();
                        food.Description = reader[2].ToString();
                        food.TypeMeal = reader[3].ToString();
                        food.Brand = reader[4].ToString();

                        foods.Add(food);
                    }
                }

                connection.Close();

                if(foods.Count() > 0)
                {
                    return Ok(foods);
                }
                return BadRequest();
            } 
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
