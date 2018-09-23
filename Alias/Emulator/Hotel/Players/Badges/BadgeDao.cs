using System.Collections.Generic;
using System.Threading.Tasks;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Players.Badges
{
	internal class BadgeDao : AbstractDao
	{
		internal async Task<Dictionary<string, BadgeDefinition>> ReadPlayerBadgesByIdAsync(int id)
		{
			Dictionary<string, BadgeDefinition> badges = new Dictionary<string, BadgeDefinition>();
			await SelectAsync(async reader =>
			{
				while (await reader.ReadAsync())
				{
					BadgeDefinition badge = new BadgeDefinition(reader);
					if (badges.ContainsKey(badge.Code))
					{
						badges.Add(badge.Code, badge);
					}
				}
			}, "SELECT `badge_code`, `slot_id` FROM `habbo_badges` WHERE `user_id` = @0", id);
			return badges;
		}

		internal async Task AddPlayerBadgeAsync(string code, int userId)
		{
			await InsertAsync("INSERT INTO `habbo_badges` (`badge_code`, `user_id`) VALUES (@0, @1);", code, userId);
		}

		internal async Task RemovePlayerBadgeAsync(string code, int userId)
		{
			await InsertAsync("DELETE FROM `habbo_badges` WHERE `badge_code` = @0 AND `user_id` = @1;", code, userId);
		}

		internal async Task UpdatePlayerBadgeAsync(BadgeDefinition badge, string oldCode, int userId)
		{
			await InsertAsync("UPDATE `habbo_badges` SET `badge_code` = @0, `slot_id` = @1 WHERE `badge_code` = @2 AND `user_id` = @3;", badge.Code, badge.Slot, oldCode, userId);
		}
	}
}
