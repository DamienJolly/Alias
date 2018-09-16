/*
 * Deprecated!
using System.Collections.Generic;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Achievements
{
    class AchievementDatabase
	{
		public static Dictionary<string, Achievement> ReadAchievements()
		{
			Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();
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
							Levels   = ReadLevels(Reader.GetInt32("id"))
						};
						if (!achievements.ContainsKey(achievement.Name))
						{
							achievements.Add(achievement.Name, achievement);
						}
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
	}
}
*/
