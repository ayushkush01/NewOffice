using Office.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace Office.Persistance
{
    public static class EmployeeCrud
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        public static List<Employee> GetEmployees()
        {
            var Employees = new List<Employee>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetEmployeeData";

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Employees.Add(new Employee()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                            TeamId = reader.GetInt32(reader.GetOrdinal("TeamId")),
                            IsInsured = reader.GetBoolean(reader.GetOrdinal("IsInsured")),
                            Gender = reader.GetString(reader.GetOrdinal("Gender")),
                            Address = reader.GetString(reader.GetOrdinal("Address")),
                            State = reader.GetString(reader.GetOrdinal("State")),
                            DateOfBirth = !reader.IsDBNull(reader.GetOrdinal("DateOfBirth")) ? reader.GetDateTime(reader.GetOrdinal("DateOfBirth")) : DateTime.Now,
                            JoiningDate = !reader.IsDBNull(reader.GetOrdinal("JoiningDate")) ? reader.GetDateTime(reader.GetOrdinal("JoiningDate")) : DateTime.Now,
                            Department = DepartmentCrud.GetDepartmentById(reader.GetInt32(reader.GetOrdinal("DepartmentId"))),
                            Team = TeamCrud.GetTeamById(reader.GetInt32(reader.GetOrdinal("TeamId"))),
                            Skills = EmployeeSkillCrud.GetEmployeeSkillByEmpId(reader.GetInt32(reader.GetOrdinal("Id"))),
                            Projects = EmployeeProjectCrud.GetEmployeeProjectByEmpId(reader.GetInt32(reader.GetOrdinal("Id")))
                        });
                    }
                    return Employees;
                }
            }
        }

        public static Employee GetEmployeeById(int id)
        {
            Employee Employee = GetEmployees().ToList().Where(e=>e.Id == id).SingleOrDefault();

            return Employee;
        }

        public static void InsertOrUpdateEmployeesWithProjectsAndSkills(Employee Employee, int[] ProjectIds, int[] SkillIds)
        {
            string projectIds;
            string skillIds;
            if (ProjectIds != null)
            {
                projectIds = string.Join(",", ProjectIds);
            }
            else
            {
                projectIds = string.Empty;
            }
            if (SkillIds != null)
            {
                skillIds = string.Join(",", SkillIds);
            }
            else
            {
                skillIds = string.Empty;
            }
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "InserOrUpdateEmployeeWithProjectsAndSkills";

                        command.Parameters.AddWithValue("@DepartmentId", Employee.DepartmentId);
                        command.Parameters.AddWithValue("@FirstName", Employee.FirstName);
                        command.Parameters.AddWithValue("@LastName", Employee.LastName);
                        command.Parameters.AddWithValue("@State", Employee.State);
                        command.Parameters.AddWithValue("@TeamId", Employee.TeamId);
                        command.Parameters.AddWithValue("@Gender", Employee.Gender);
                        command.Parameters.AddWithValue("@Address", Employee.Address);
                        command.Parameters.AddWithValue("@DateOfBirth", Employee.DateOfBirth);
                        command.Parameters.AddWithValue("@JoiningDate", Employee.JoiningDate);
                        command.Parameters.AddWithValue("@IsInsured", Employee.IsInsured);
                        command.Parameters.AddWithValue("@SkillIds", skillIds);
                        command.Parameters.AddWithValue("@ProjectIds", projectIds);
                        command.Parameters.AddWithValue("@EmployeeId", Employee.Id);

                        command.ExecuteNonQuery();                   
                }
            }
        }

        public static void DeleteEmployeeWithProjectsAndSkills(int EmpId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    if (EmpId != 0)
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "DeleteEmployee";
                        command.Parameters.AddWithValue("@EmployeeId", EmpId);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                }
            }
        }
    }
}