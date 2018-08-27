using System.Collections.Generic;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Users.Achievements
{
    class AchievementDatabase
    {
		public static Dictionary<string, int> ReadAchievements(int userId)
		{
			Dictionary<string, int> achievements = new Dictionary<string, int>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", userId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `progress`, `name` FROM `habbo_achievements` WHERE `user_id` = @id"))
				{
					while (Reader.Read())
					{
						if (!achievements.ContainsKey(Reader.GetString("name")))
						{
							achievements.Add(Reader.GetString("name"), Reader.GetInt32("progress"));
						}
					}
				}
			}
			return achievements;
		}

		public static void SaveAchievements(AchievementComponent achievement)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				foreach (var ach in achievement.Achievements)
				{
					dbClient.AddParameter("userId", achievement.Habbo().Id);
					dbClient.AddParameter("name", ach.Key);
					dbClient.AddParameter("progress", ach.Value);
					dbClient.Query("INSERT INTO `habbo_achievements` (`user_id`, `name`, `progress`) VALUES (@userId, @name, @progress) ON DUPLICATE KEY UPDATE `progress` = @progress");
				}
			}
		}
	}
}
