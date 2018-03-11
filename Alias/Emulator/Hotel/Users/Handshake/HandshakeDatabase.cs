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
			Habbo habbo = null;
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
					habbo = new Habbo()
					{
						Id = (int)row["id"],
						Username = (string)row["username"],
						Mail = (string)row["mail"],
						Look = (string)row["look"],
						Motto = (string)row["motto"],
						Gender = (string)row["gender"],
						Rank = 6,
						ClubLevel = 1,
						Credits = 9999,
						HomeRoom = 0,
						AchievementScore = 10,
						Muted = false,
						AllowTrading = true
					};
				}
			}
			
			return habbo;
		}

		public static bool IsBanned(int userId)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", userId);
				dbClient.AddParameter("now", AliasEnvironment.GetUnixTimestamp());
				return dbClient.DataRow("SELECT `reason` FROM `bans` WHERE `user_id` = @id AND `expires` > @now") != null;
			}
		}
	}
}
