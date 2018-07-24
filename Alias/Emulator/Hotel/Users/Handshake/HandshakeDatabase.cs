using System.Collections.Generic;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Users.Handshake
{
	class HandshakeDatabase
	{
		public static bool SSOExists(string sso)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("sso", sso);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT null FROM `habbos` WHERE `username` = @sso"))
				{
					return Reader.Read();
				}
			}
		}

		public static Habbo BuildHabbo(string sso)
		{
			int userId = 0;

			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("sso", sso);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `id` FROM `habbos` WHERE `username` = @sso"))
				{
					if (Reader.Read())
					{
						userId = Reader.GetInt32("id");
					}
				}
			}

			return BuildHabbo(userId);
		}

		public static Habbo BuildHabbo(int userId)
		{
			Habbo habbo = null;
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", userId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `habbos` WHERE `id` = @id"))
				{
					if (Reader.Read())
					{
						habbo = new Habbo()
						{
							Id               = Reader.GetInt32("id"),
							Username         = Reader.GetString("username"),
							Mail             = Reader.GetString("mail"),
							Look             = Reader.GetString("look"),
							Motto            = Reader.GetString("motto"),
							Gender           = Reader.GetString("gender"),
							Rank             = Reader.GetInt32("rank"),
							ClubLevel        = Reader.GetInt32("club_level"),
							Credits          = Reader.GetInt32("credits"),
							HomeRoom         = Reader.GetInt32("home_room"),
							AchievementScore = Reader.GetInt32("achievement_score"),
							Muted            = false,
							AllowTrading     = true,
							GroupId          = Reader.GetInt32("group_id"),
							Groups           = ReadGroups(Reader.GetInt32("id"))
						};
					}
				}
			}
			
			return habbo;
		}

		public static List<int> ReadGroups(int userId)
		{
			List<int> groups = new List<int>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", userId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `group_id` FROM `group_members` WHERE `user_id` = @id AND `rank` <= 2"))
				{
					while (Reader.Read())
					{
						groups.Add(Reader.GetInt32("group_id"));
					}
				}
			}
			return groups;
		}

		public static bool IsBanned(int userId)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", userId);
				dbClient.AddParameter("now", Alias.GetUnixTimestamp());
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT null FROM `bans` WHERE `user_id` = @id AND `expires` > @now"))
				{
					return Reader.Read();
				}
			}
		}
	}
}
