using System.Data;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Users.Handshake
{
	public class HandshakeDatabase
	{
		public static bool SSOExists(string sso)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("sso", sso);
				return dbClient.DataRow("SELECT `username` FROM `habbos` WHERE `username` = @sso") != null;
			}
		}

		public static Habbo BuildHabbo(string sso)
		{
			int userId = 0;

			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("sso", sso);
				userId = dbClient.Int32("SELECT `id` FROM `habbos` WHERE `username` = @sso");
			}

			return BuildHabbo(userId);
		}

		public static Habbo BuildHabbo(int userId)
		{
			Habbo habbo = new Habbo();

			if (userId == 0)
			{
				return habbo;
			}

			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", userId);
				DataRow row = dbClient.DataRow("SELECT * FROM `habbos` WHERE `id` = @id");
				if (row != null)
				{
					habbo.Id = (int)row["id"];
					habbo.Username = (string)row["username"];
					habbo.Mail = (string)row["mail"];
					habbo.Look = (string)row["look"];
					habbo.Motto = (string)row["motto"];
					habbo.Gender = (string)row["gender"];
				}
			}
			
			return habbo;
		}

		public static bool IsBanned(int userId)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", userId);
				dbClient.AddParameter("now", AliasEnvironment.Time());
				return dbClient.DataRow("SELECT `reason` FROM `bans` WHERE `user_id` = @id AND `expires` > @now") != null;
			}
		}
	}
}
