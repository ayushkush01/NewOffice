using Office.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Office.Persistance
{
    public static class SkillsCrud
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        public static List<Skill> GetSkills()
        {
            var Skills = new List<Skill>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetSkills";

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Skills.Add(new Skill
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                        });
                    }
                }
            }
            return Skills;
        }

        public static Skill GetSkillById(int id)
        {
            var Skills = GetSkills();
            var Skill = Skills.Find(x => x.Id == id);
            return Skill;
        }
    }
}