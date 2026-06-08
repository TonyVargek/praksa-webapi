using Example.Common;
using Example.Model;
using Example.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Repository
{
    public class MemberRepository : IMemberRepository
    {
        string CONNECTION_STRING = "Host=localhost;Port=5432;Database=testdb2;Username=postgres;Password=postgres";
        public bool Add(Member member)
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

        public bool Add(List<Member> members)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);
                StringBuilder sb = new StringBuilder();

                sb.Append("INSERT INTO \"Member\" (\"FirstName\", \"LastName\", \"Weight\", \"Height\", \"BMI\") VALUES ");
                using NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;

                for (int i = 0; i < members.Count(); i++)
                {
                    sb.Append($"(@firstname{i}, @lastname{i}, @weight{i}, @height{i}, @bmi{i})");

                    if (i != members.Count - 1)
                    {
                        sb.Append(", ");
                    }

                    command.Parameters.AddWithValue($"@firstname{i}", members[i].FirstName);
                    command.Parameters.AddWithValue($"@lastname{i}", members[i].LastName);
                    command.Parameters.AddWithValue($"@weight{i}", members[i].Weight);
                    command.Parameters.AddWithValue($"@height{i}", members[i].Height);
                    command.Parameters.AddWithValue($"@bmi{i}", members[i].BMI);
                }

                command.CommandText = sb.ToString();

                connection.Open();

                int affectedRows = command.ExecuteNonQuery();

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

        public bool Delete(int id)
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
                    return true;
                }
                return false;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Member> GetAll(MemberFilter filter)
        {
            try
            {
                List<Member> members = new List<Member>();
                using NpgsqlConnection connection = new NpgsqlConnection(CONNECTION_STRING);

                StringBuilder sb = new StringBuilder();

                sb.Append("SELECT m.*, f.\"Name\", f.\"Description\", f.\"TypeMeal\", f.\"Brand\" FROM \"Member\" as m LEFT JOIN \"Food\" as f on m.\"FoodId\" = f.\"Id\" WHERE 1 = 1 ");
                using NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;

                if (!string.IsNullOrEmpty(filter.FirstName))
                {
                    sb.Append(" AND m.\"FirstName\" = @firstname");
                    command.Parameters.AddWithValue("@firstname", filter.FirstName);
                }
                if (!string.IsNullOrEmpty(filter.LastName))
                {
                    sb.Append(" AND m.\"LastName\" = @lastname");
                    command.Parameters.AddWithValue("@lastname", filter.LastName);
                }
                if (filter.BMI.HasValue)
                {
                    sb.Append(" AND m.\"BMI\" > @bmi");
                    command.Parameters.AddWithValue("@bmi", Convert.ToDecimal(filter.BMI));
                }
                if (!string.IsNullOrEmpty(filter.FavoriteFood))
                {
                    sb.Append(" AND f.\"Name\" = @favoriteFood");
                    command.Parameters.AddWithValue("@favoriteFood", filter.FavoriteFood);
                }

                command.CommandText = sb.ToString();

                connection.Open();

                NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
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

                        if (member.FoodId > 0)
                        {
                            Food food = new Food();
                            food.Id = member.FoodId;
                            food.Name = reader[7].ToString();
                            food.Description = reader[8].ToString();
                            food.TypeMeal = reader[9].ToString();
                            food.Brand = reader[9].ToString();
                            member.Food = food;
                        }
                        if (member.Id > 0)
                        {
                            members.Add(member);
                        }
                    }
                }

                connection.Close();

                if (members.Count() > 0)
                {
                    return members;
                }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Member GetById(int id)
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

                    if (member.FoodId > 0)
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

                if (member.Id > 0)
                {
                    return member;
                }

                return null;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool Update(int id, Member newMember)
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
