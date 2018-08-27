using System.Collections.Generic;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Users.Achievements
{
    class AchievementDatabase
    {
		public static Dictionary<int, int> ReadAchievements(int userId)
		{
			Dictionary<int, int> achievements = new Dictionary<int, int>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", userId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `achievement_id`, `progress` FROM `habbo_achievements` WHERE `user_id` = @id"))
				{
					while (Reader.Read())
					{
						if (!achievements.ContainsKey(Reader.GetInt32("achievement_id")))
						{
							achievements.Add(Reader.GetInt32("achievement_id"), Reader.GetInt32("progress"));
						}
					}
				}
			}
			return achievements;
		}

		public static void AddAchievement(int id, int amount, int userId)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", id);
				dbClient.AddParameter("amount", amount);
				dbClient.AddParameter("userId", userId);
				dbClient.Query("INSERT INTO `habbo_achievements` (`achievement_id`, `progress`, `user_id`) VALUES (@id, @amount, @userId)");
			}
		}

		public static void UpdateAchievement(int id, int amount, int userId)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", id);
				dbClient.AddParameter("amount", amount);
				dbClient.AddParameter("userId", userId);
				dbClient.Query("UPDATE `habbo_achievements` SET `progress` = @amount WHERE `achievement_id` = @id AND `user_id` = @userId");
			}
		}
	}
}
