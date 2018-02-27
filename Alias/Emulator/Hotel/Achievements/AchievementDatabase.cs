using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Users;

namespace Alias.Emulator.Hotel.Achievements
{
    public class AchievementDatabase
	{
		public static List<Achievement> ReadAchievements()
		{
			List<Achievement> achievements = new List<Achievement>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				foreach (DataRow row in dbClient.DataTable("SELECT * FROM `achievements`").Rows)
				{
					Achievement achievement = new Achievement()
					{
						Id    = (int)row["id"],
						Name     = (string)row["name"],
						Category = AchievementCategories.GetCategoryFromString((string)row["category"]),
						Levels   = AchievementDatabase.ReadLevels((int)row["id"])
					};
					achievements.Add(achievement);
					row.Delete();
				}
			}
			return achievements;
		}

		public static List<AchievementLevel> ReadLevels(int id)
		{
			List<AchievementLevel> levels = new List<AchievementLevel>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", id);
				foreach (DataRow row in dbClient.DataTable("SELECT * FROM `achievement_levels` WHERE `id` = @id").Rows)
				{
					AchievementLevel level = new AchievementLevel()
					{
						Level        = (int)row["level"],
						RewardAmount = (int)row["reward_amount"],
						RewardType   = (int)row["reward_type"],
						RewardPoints = (int)row["reward_points"],
						Progress     = (int)row["progress"]
					};
					levels.Add(level);
					row.Delete();
				}
			}
			return levels;
		}

		public static void AddUserAchievement(Habbo habbo, Achievement achievement, int amount)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("userId", habbo.Id);
				dbClient.AddParameter("name", achievement.Name);
				dbClient.AddParameter("progress", amount);
				dbClient.Query("INSERT INTO `habbo_achievements` (`user_id`, `name`, `progress`) VALUES (@userId, @name, @progress) ON DUPLICATE KEY UPDATE `progress` = `progress` + @progress");
			}
		}
	}
}
