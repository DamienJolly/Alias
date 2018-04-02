using System.Data;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Users.Handshake
{
	public class HandshakeDatabase
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
							Rank             = 6,
							ClubLevel        = 1,
							Credits          = 9999,
							HomeRoom         = 0,
							AchievementScore = 10,
							Muted            = false,
							AllowTrading     = true
						};
					}
				}
			}
			
			return habbo;
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
