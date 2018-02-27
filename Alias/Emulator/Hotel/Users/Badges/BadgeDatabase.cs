using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Users.Badges
{
    public class BadgeDatabase
    {
		public static List<BadgeDefinition> InitBadges(int userId)
		{
			List<BadgeDefinition> badges = new List<BadgeDefinition>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("userId", userId);
				foreach (DataRow row in dbClient.DataTable("SELECT `badge_code`, `slot_id` FROM `habbo_badges` WHERE `user_id` = @userId").Rows)
				{
					BadgeDefinition badge = new BadgeDefinition()
					{
						Code = (string)row["badge_code"],
						Slot = (int)row["slot_id"]
					};
					badges.Add(badge);
					row.Delete();
				}
			}
			return badges;
		}

		public static void GiveBadge(Habbo habbo, string code)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("userId", habbo.Id);
				dbClient.AddParameter("badgeCode", code);
				dbClient.Query("INSERT INTO `habbo_badges` (`badge_code`, `user_id`) VALUES (@badgeCode, @userId)");
			}
		}

		public static void TakeBadge(Habbo habbo, string code)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("userId", habbo.Id);
				dbClient.AddParameter("badgeCode", code);
				dbClient.Query("DELETE FROM `habbo_badges` WHERE `badge_code` = @badgeCode AND `user_id` = @userId");
			}
		}

		public static void UpdateBadge(Habbo habbo, BadgeDefinition badge, string code)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("userId", habbo.Id);
				dbClient.AddParameter("badgeCode", code);
				dbClient.AddParameter("newBadgeCode", badge.Code);
				dbClient.AddParameter("newBadgeSlot", badge.Slot);
				dbClient.Query("UPDATE `habbo_badges` SET `badge_code` = @newBadgeCode,	`slot_id` = @newBadgeSlot WHERE `badge_code` = @badgeCode AND `user_id` = @userId");
			}
		}
	}
}
