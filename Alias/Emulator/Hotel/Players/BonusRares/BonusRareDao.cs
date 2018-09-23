using System.Collections.Generic;
using System.Threading.Tasks;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Players.BonusRares
{
    internal class BonusRareDao : AbstractDao
    {
		internal async Task<Dictionary<int, int>> ReadBonusRaresAsync(int id)
		{
			Dictionary<int, int> bonusRares = new Dictionary<int, int>();
			await SelectAsync(async reader =>
			{
				while (await reader.ReadAsync())
				{
					if (bonusRares.ContainsKey(reader.ReadData<int>("bonus_id")))
					{
						bonusRares.Add(reader.ReadData<int>("bonus_id"), reader.ReadData<int>("progress"));
					}
				}
			}, "SELECT * FROM `habbo_bonus_rares` WHERE `user_id` = @0", id);
			return bonusRares;
		}

		internal async Task AddBonusRaresAsync(int bonusId, int progress, int userId)
		{
			await InsertAsync("INSERT INTO `habbo_bonus_rares` (`bonus_id`, `progress`, `user_id`) VALUES (@0, @1, @2);", bonusId, progress, userId);
		}

		internal async Task UpdateBonusRaresAsync(int bonusId, int progress, int userId)
		{
			await InsertAsync("UPDATE `habbo_bonus_rares` SET `progress` = @1 WHERE `bonus_id` = @0 AND `user_id` = @2;", bonusId, progress, userId);
		}
	}
}
