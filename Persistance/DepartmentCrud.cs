using Office.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace Office.Persistance
{
    public static class DepartmentCrud
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        public static List<Department> GetDepartments()
        {
            var departments = new List<Department>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetDepartments";

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        departments.Add(new Department
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                        });
                    }
                }
            }
            return departments;
        }

        public static Department GetDepartmentById(int id)
        {
            var departments = GetDepartments();
            var department = departments.Find(x => x.Id == id);
            return department;
        }
    }
}