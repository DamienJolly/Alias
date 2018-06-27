using System.Collections.Generic;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Users;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Achievements
{
    class AchievementDatabase
	{
		public static List<Achievement> ReadAchievements()
		{
			List<Achievement> achievements = new List<Achievement>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `achievements`"))
				{
					while (Reader.Read())
					{
						Achievement achievement = new Achievement()
						{
							Id       = Reader.GetInt32("id"),
							Name     = Reader.GetString("name"),
							Category = AchievementCategories.GetCategoryFromString(Reader.GetString("category")),
							Levels   = AchievementDatabase.ReadLevels(Reader.GetInt32("id"))
						};
						achievements.Add(achievement);
					}
				}
			}
			return achievements;
		}

		public static List<AchievementLevel> ReadLevels(int id)
		{
			List<AchievementLevel> levels = new List<AchievementLevel>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", id);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `achievement_levels` WHERE `id` = @id"))
				{
					while (Reader.Read())
					{
						AchievementLevel level = new AchievementLevel()
						{
							Level        = Reader.GetInt32("level"),
							RewardAmount = Reader.GetInt32("reward_amount"),
							RewardType   = Reader.GetInt32("reward_type"),
							RewardPoints = Reader.GetInt32("reward_points"),
							Progress     = Reader.GetInt32("progress")
						};
						levels.Add(level);
					}
				}
			}
			return levels;
		}

		public static void AddUserAchievement(Habbo habbo, Achievement achievement, int amount)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("userId", habbo.Id);
				dbClient.AddParameter("name", achievement.Name);
				dbClient.AddParameter("progress", amount);
				dbClient.Query("INSERT INTO `habbo_achievements` (`user_id`, `name`, `progress`) VALUES (@userId, @name, @progress) ON DUPLICATE KEY UPDATE `progress` = `progress` + @progress");
			}
		}
	}
}
