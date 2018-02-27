using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Achievements;

namespace Alias.Emulator.Hotel.Users.Achievements
{
    public class AchievementDatabase
    {
		public static void InitAchievements(Achievement achievement)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", achievement.Habbo().Id);
				foreach (DataRow row in dbClient.DataTable("SELECT `progress`, `name` FROM `habbo_achievements` WHERE `user_id` = @id").Rows)
				{
					AchievementProgress ach = new AchievementProgress()
					{
						Achievement = AchievementManager.GetAchievement((string)row["name"]),
						Progress    = (int)row["progress"]
					};
					achievement.RequestAchievementProgress().Add(ach);
					row.Delete();
				}
			}
		}

		public static void SaveAchievements(Achievement achievement)
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
