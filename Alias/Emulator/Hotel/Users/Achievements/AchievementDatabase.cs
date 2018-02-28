using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Achievements;

namespace Alias.Emulator.Hotel.Users.Achievements
{
    public class AchievementDatabase
    {
		public static List<AchievementProgress> ReadAchievements(int userId)
		{
			List<AchievementProgress> achievements = new List<AchievementProgress>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", userId);
				foreach (DataRow row in dbClient.DataTable("SELECT `progress`, `name` FROM `habbo_achievements` WHERE `user_id` = @id").Rows)
				{
					AchievementProgress achievement = new AchievementProgress()
					{
						Achievement = AchievementManager.GetAchievement((string)row["name"]),
						Progress    = (int)row["progress"]
					};
					achievements.Add(achievement);
					row.Delete();
				}
			}
			return achievements;
		}

		public static void SaveAchievements(AchievementComponent achievement)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				foreach (AchievementProgress ach in achievement.RequestAchievementProgress())
				{
					dbClient.AddParameter("userId", achievement.Habbo().Id);
					dbClient.AddParameter("name", ach.Achievement.Name);
					dbClient.AddParameter("progress", ach.Progress);
					dbClient.Query("INSERT INTO `habbo_achievements` (`user_id`, `name`, `progress`) VALUES (@userId, @name, @progress) ON DUPLICATE KEY UPDATE `progress` = @progress");
					dbClient.ClearParameters();
				}
			}
		}
	}
}
