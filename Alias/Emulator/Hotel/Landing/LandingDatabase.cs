using System.Collections.Generic;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Items;
using Alias.Emulator.Hotel.Landing.Competition;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Landing
{
	class LandingDatabase
	{
		public static List<LandingArticle> ReadArticles()
		{
			List<LandingArticle> articles = new List<LandingArticle>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `landing_articles`"))
				{
					while (Reader.Read())
					{
						LandingArticle article = new LandingArticle()
						{
							Id = Reader.GetInt32("id"),
							Title = Reader.GetString("title"),
							Message = Reader.GetString("text"),
							Caption = Reader.GetString("caption"),
							Type = Reader.GetInt32("type"),
							Link = Reader.GetString("link"),
							Image = Reader.GetString("image")
						};
						articles.Add(article);
					}
				}
			}
			return articles;
		}

		public static Dictionary<int, LandingBonusRare> ReadBonusRares()
		{
			Dictionary<int, LandingBonusRare> bonusRares = new Dictionary<int, LandingBonusRare>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `landing_bonus_rares`"))
				{
					while (Reader.Read())
					{
						ItemData prize = Alias.Server.ItemManager.GetItemData(Reader.GetInt32("item_id"));
						if (prize == null)
						{
							continue;
						}
						LandingBonusRare bonusRare = new LandingBonusRare()
						{
							Name = Reader.GetString("name"),
							Prize = prize,
							Goal = Reader.GetInt32("goal")
						};
						if (!bonusRares.ContainsKey(Reader.GetInt32("id")))
						{
							bonusRares.Add(Reader.GetInt32("id"), bonusRare);
						}
					}
				}
			}
			return bonusRares;
		}

		public static Dictionary<int, LandingCompetition> ReadCompetitions()
		{
			Dictionary<int, LandingCompetition> competitions = new Dictionary<int, LandingCompetition>();
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
						if (!competitions.ContainsKey(Reader.GetInt32("id")))
						{
							competitions.Add(Reader.GetInt32("id"), competiton);
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
