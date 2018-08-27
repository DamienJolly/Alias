using System.Collections.Generic;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Landing.Competition;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Landing
{
	class LandingDatabase
	{
		public static Dictionary<string, LandingCompetition> ReadCompetitions()
		{
			Dictionary<string, LandingCompetition> competitions = new Dictionary<string, LandingCompetition>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `landing_competitions`"))
				{
					while (Reader.Read())
					{
						LandingCompetition competiton = new LandingCompetition()
						{
							Name = Reader.GetString("name"),
							Users = ReadCompetitionUsers(Reader.GetString("query"))
						};
						if (!competitions.ContainsKey(competiton.Name))
						{
							competitions.Add(competiton.Name, competiton);
						}
					}
				}
			}
			return competitions;
		}

		public static List<CompetitionUser> ReadCompetitionUsers(string query)
		{
			List<CompetitionUser> users = new List<CompetitionUser>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader(query))
				{
					while (Reader.Read())
					{
						CompetitionUser user = new CompetitionUser
						{
							Id = Reader.GetInt32("id"),
							Username = Reader.GetString("username"),
							Figure = Reader.GetString("look"),
							Points = Reader.GetInt32("points")
						};
						users.Add(user);
					}
				}
			}
			return users;
		}
	}
}
