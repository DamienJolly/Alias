using System.Collections.Generic;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Users.Badges
{
    public class BadgeDatabase
    {
		public static List<BadgeDefinition> InitBadges(int userId)
		{
			List<BadgeDefinition> badges = new List<BadgeDefinition>();
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
			{
				dbClient.AddParameter("userId", userId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `badge_code`, `slot_id` FROM `habbo_badges` WHERE `user_id` = @userId"))
				{
					while (Reader.Read())
					{
						BadgeDefinition badge = new BadgeDefinition()
						{
							Code = Reader.GetString("badge_code"),
							Slot = Reader.GetInt32("slot_id")
						};
						badges.Add(badge);
					}
				}
			}
			return badges;
		}

		public static void GiveBadge(Habbo habbo, string code)
		{
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
			{
				dbClient.AddParameter("userId", habbo.Id);
				dbClient.AddParameter("badgeCode", code);
				dbClient.Query("INSERT INTO `habbo_badges` (`badge_code`, `user_id`) VALUES (@badgeCode, @userId)");
			}
		}

		public static void TakeBadge(Habbo habbo, string code)
		{
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
			{
				dbClient.AddParameter("userId", habbo.Id);
				dbClient.AddParameter("badgeCode", code);
				dbClient.Query("DELETE FROM `habbo_badges` WHERE `badge_code` = @badgeCode AND `user_id` = @userId");
			}
		}

		public static void UpdateBadge(Habbo habbo, BadgeDefinition badge, string code)
		{
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
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
