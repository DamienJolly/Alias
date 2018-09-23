using System.Collections.Generic;
using System.Threading.Tasks;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Players.Achievements
{
	internal class AchievementDao : AbstractDao
    {
		internal async Task<Dictionary<int, int>> ReadPlayerAchievementsByIdAsync(int id)
		{
			Dictionary<int, int> achievements = new Dictionary<int, int>();
			await SelectAsync(async reader =>
			{
				while (await reader.ReadAsync())
				{
					if (!achievements.ContainsKey(reader.ReadData<int>("achievement_id")))
					{
						achievements.Add(reader.ReadData<int>("achievement_id"), reader.ReadData<int>("progress"));
					}
				}
			}, "SELECT `achievement_id`, `progress` FROM `habbo_achievements` WHERE `user_id` = @0;", id);
			return achievements;
		}

		internal async Task AddPlayerAchievementAsync(int id, int amount, int userId)
		{
			await InsertAsync("INSERT INTO `habbo_achievements` (`achievement_id`, `progress`, `user_id`) VALUES (@0, @1, @2);", id, amount, userId);
		}

		internal async Task UpdatePlayerAchievementAsync(int id, int amount, int userId)
		{
			await InsertAsync("UPDATE `habbo_achievements` SET `progress` = @1 WHERE `achievement_id` = @0 AND `user_id` = @2;", id, amount, userId);
		}
	}
}
