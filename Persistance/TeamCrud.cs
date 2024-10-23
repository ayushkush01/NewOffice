using Office.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Office.Persistance
{
    public static class TeamCrud
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        public static List<Team> GetTeams()
        {
            var Teams = new List<Team>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetTeams";

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Teams.Add(new Team
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                        });
                    }
                }
            }
            return Teams;
        }

        public static Team GetTeamById(int id)
        {
            var Teams = GetTeams();
            var Team = Teams.Find(x => x.Id == id);
            return Team;
        }
    }
}