using Office.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace Office.Persistance
{
    public static class EmployeeProjectCrud
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        public static List<EmployeeProject> GetEmployeeProject()
        {
            var Projects = new List<EmployeeProject>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetEmployeeProject";

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Projects.Add(new EmployeeProject
                        {
                            ProjectId = reader.GetInt32(reader.GetOrdinal("ProjectId")),
                            EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                            ProjectName = reader.GetString(reader.GetOrdinal("ProjectName")),
                            EmployeeName = reader.GetString(reader.GetOrdinal("EmployeeName")),
                        });
                    }
                }
            }

            return Projects;
        }
        public static List<Project> GetEmployeeProjectByEmpId(int id)
        {
            var EmpProjects = GetEmployeeProject();
            EmpProjects = EmpProjects.Where(c => c.EmployeeId == id).ToList();

            var Projects = new List<Project>();
            foreach (var c in EmpProjects)
            {
                Projects.Add(new Project
                {
                    Id = c.ProjectId,
                    Name = c.ProjectName,
                });
            }
            return Projects;
        }
    }
}