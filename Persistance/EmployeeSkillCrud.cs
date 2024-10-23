using Office.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace Office.Persistance
{
    public static class EmployeeSkillCrud
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        public static List<EmployeeSkill> GetEmployeeSkill()
        {
            var Skills = new List<EmployeeSkill>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetEmployeeSkill";

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Skills.Add(new EmployeeSkill
                        {
                            SkillId = reader.GetInt32(reader.GetOrdinal("SkillId")),
                            EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                            SkillName = reader.GetString(reader.GetOrdinal("SkillName")),
                            EmployeeName = reader.GetString(reader.GetOrdinal("EmployeeName")),
                        });
                    }
                }
            }

            return Skills;
        }
        public static List<Skill> GetEmployeeSkillByEmpId(int id)
        {
            var EmpSkills = GetEmployeeSkill();
            EmpSkills = EmpSkills.Where(c => c.EmployeeId == id).ToList();

            var Skills = new List<Skill>();
            foreach (var c in EmpSkills)
            {
                Skills.Add(new Skill
                {
                    Id = c.SkillId,
                    Name = c.SkillName,
                });
            }
            return Skills;
        }
    }
}