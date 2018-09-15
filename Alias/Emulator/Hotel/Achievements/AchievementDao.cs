using Alias.Emulator.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alias.Emulator.Hotel.Achievements
{
	internal class AchievementDao : AbstractDao
    {
		internal async Task<Dictionary<string, Achievement>> LoadAchievementsAsync()
		{
			Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();

			await SelectAsync(async reader =>
			{
				while (await reader.ReadAsync())
				{
					Achievement achievement = new Achievement()
					{
						Id = reader.ReadData<int>("id"),
						Name = reader.ReadData<string>("name"),
						Category = AchievementCategories.GetCategoryFromString(reader.ReadData<string>("category")),
						Levels = await ReadLevels(reader.ReadData<int>("id"))
					};
					if (!achievements.ContainsKey(achievement.Name))
					{
						achievements.Add(achievement.Name, achievement);
					}
				}
			}, "SELECT * FROM `achievements`");

			return achievements;
		}

		internal async Task<List<AchievementLevel>> ReadLevels(int id)
		{
			List<AchievementLevel> levels = new List<AchievementLevel>();

			await SelectAsync(async reader =>
			{
				while (await reader.ReadAsync())
				{
					AchievementLevel level = new AchievementLevel()
					{
						Level = reader.ReadData<int>("level"),
						RewardAmount = reader.ReadData<int>("reward_amount"),
						RewardType = reader.ReadData<int>("reward_type"),
						RewardPoints = reader.ReadData<int>("reward_points"),
						Progress = reader.ReadData<int>("progress")
					};

					levels.Add(level);
				}
			}, "SELECT * FROM `achievement_levels` WHERE `id` = @0", id);

			return levels;
		}
    }
}
