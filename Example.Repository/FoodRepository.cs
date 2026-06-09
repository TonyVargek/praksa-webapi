using Example.Common;
using Example.Model;
using Example.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Example.Repository
{
    public class FoodRepository : IFoodRepository
    {
        string CONNECTION_STRING = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123";
        public async Task<bool> AddAsync(Food food)
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

                var rowsAffected = await command.ExecuteNonQueryAsync();

                connection.Close();

                if (rowsAffected > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> AddAsync(List<Food> foods)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);
                StringBuilder sb = new StringBuilder();

                sb.Append("INSERT INTO \"Food\" (\"Name\", \"Description\", \"TypeMeal\", \"Brand\") VALUES ");
                using NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                for (int i = 0; i < foods.Count(); i++)
                {
                    sb.Append($"(@name{i}, @description{i}, @typemeal{i}, @brand{i})");

                    if (i != foods.Count - 1)
                    {
                        sb.Append(", ");
                    }

                    command.Parameters.AddWithValue($"@name{i}", foods[i].Name);
                    command.Parameters.AddWithValue($"@description{i}", foods[i].Description);
                    command.Parameters.AddWithValue($"@typemeal{i}", foods[i].TypeMeal);
                    command.Parameters.AddWithValue($"@brand{i}", foods[i].Brand);
                }

                command.CommandText = sb.ToString();

                connection.Open();

                var rowsAffected = await command.ExecuteNonQueryAsync();

                connection.Close();

                if (rowsAffected > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);

                string comm = "DELETE FROM \"Food\" WHERE \"Id\" = @id";
                using NpgsqlCommand command = new NpgsqlCommand(comm, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                int affectedRows = await command.ExecuteNonQueryAsync();

                connection.Close();

                if (affectedRows > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<List<Food>> GetAllAsync(FoodFilter filter)
        {
            try
            {
                List<Food> foods = new List<Food>();
                using NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);

                StringBuilder sb = new StringBuilder();

                sb.Append("SELECT * FROM \"Food\" WHERE 1 = 1 ");
                using NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;

                if (!string.IsNullOrEmpty(filter.TypeMeal))
                {
                    sb.Append("AND \"TypeMeal\" = @typemeal");
                    command.Parameters.AddWithValue("@typemeal", filter.TypeMeal);
                }
                if (!string.IsNullOrEmpty(filter.Brand))
                {
                    sb.Append("AND \"Brand\" = @brand");
                    command.Parameters.AddWithValue("@brand", filter.Brand);
                }

                command.CommandText = sb.ToString();

                connection.Open();
                NpgsqlDataReader reader = await command.ExecuteReaderAsync();

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
                    return foods;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Food> GetByIdAsync(int id)
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

                NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
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
                    return food;
                }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(int id, Food newFood)
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

                int affectedRows = await command.ExecuteNonQueryAsync();

                connection.Close();

                if (affectedRows > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
