using System.Collections.Generic;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Users.Achievements
{
    public class AchievementDatabase
    {
		public static List<AchievementProgress> ReadAchievements(int userId)
		{
			List<AchievementProgress> achievements = new List<AchievementProgress>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", userId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `progress`, `name` FROM `habbo_achievements` WHERE `user_id` = @id"))
				{
					while (Reader.Read())
					{
						AchievementProgress achievement = new AchievementProgress()
						{
							Achievement = Alias.Server.AchievementManager.GetAchievement(Reader.GetString("name")),
							Progress    = Reader.GetInt32("progress")
						};
						achievements.Add(achievement);
					}
				}
			}
			return achievements;
		}

		public static void SaveAchievements(AchievementComponent achievement)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				foreach (AchievementProgress ach in achievement.RequestAchievementProgress())
				{
					dbClient.AddParameter("userId", achievement.Habbo().Id);
					dbClient.AddParameter("name", ach.Achievement.Name);
					dbClient.AddParameter("progress", ach.Progress);
					dbClient.Query("INSERT INTO `habbo_achievements` (`user_id`, `name`, `progress`) VALUES (@userId, @name, @progress) ON DUPLICATE KEY UPDATE `progress` = @progress");
				}
			}
		}
	}
}
