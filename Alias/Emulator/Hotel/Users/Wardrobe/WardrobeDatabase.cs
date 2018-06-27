using System.Collections.Generic;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Users.Wardrobe
{
	class WardrobeDatabase
	{
		public static Dictionary<int, WardrobeItem> ReadWardrobeItems(int userId)
		{
			Dictionary<int, WardrobeItem> wardrobeItems = new Dictionary<int, WardrobeItem>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", userId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `user_id`, `slot_id`, `figure`, `gender` FROM `wardrobe` WHERE `user_id` = @id"))
				{
					while (Reader.Read())
					{
						WardrobeItem item = new WardrobeItem(Reader.GetInt32("user_id"), Reader.GetInt32("slot_id"), Reader.GetString("figure"), Reader.GetString("gender"));
						wardrobeItems.Add(item.SlotId, item);
					}
				}
			}
			return wardrobeItems;
		}

		public static void UpdateWardrobeItem(WardrobeItem item)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("userId", item.UserId);
				dbClient.AddParameter("slotId", item.SlotId);
				dbClient.AddParameter("figure", item.Figure);
				dbClient.AddParameter("gender", "");
				dbClient.Query("UPDATE `wardrobe` SET `figure` = @figure, `gender` = @gender WHERE `user_id` = @userId AND `slot_id` = @slotId");
			}
		}

		public static void InsertWardrobeItem(WardrobeItem item)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("userId", item.UserId);
				dbClient.AddParameter("slotId", item.SlotId);
				dbClient.AddParameter("figure", item.Figure);
				dbClient.AddParameter("gender", "");
				dbClient.Query("INSERT INTO `wardrobe` (`user_id`, `slot_id`, `figure`, `gender`) VALUES (@userId, @slotId, @figure, @gender)");
			}
		}
	}
}
