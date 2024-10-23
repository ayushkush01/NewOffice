using Office.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Office.Persistance
{
    public static class ProjectCrud
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        public static List<Project> GetProjects()
        {
            var Projects = new List<Project>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetProjects";

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Projects.Add(new Project
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                        });
                    }
                }
            }
            return Projects;
        }

        public static Project GetProjectById(int id)
        {
            var Projects = GetProjects();
            var Project = Projects.Find(x => x.Id == id);
            return Project;
        }
    }
}