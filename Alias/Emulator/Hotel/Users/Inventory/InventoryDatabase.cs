using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Items;

namespace Alias.Emulator.Hotel.Users.Inventory
{
	public class InventoryDatabase
	{
		public static List<InventoryItem> ReadFloorItems(int userId)
		{
			List<InventoryItem> items = new List<InventoryItem>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", userId);
				foreach (DataRow row in dbClient.DataTable("SELECT * FROM `items` WHERE `user_id` = @id AND `room_id` = 0").Rows)
				{
					InventoryItem item = new InventoryItem
					{
						Id            = (int)row["id"],
						LimitedNumber = (int)row["limited_number"],
						LimitedStack  = (int)row["limited_stack"],
						ItemData      = ItemManager.GetItemData((int)row["base_id"]),
						UserId        = (int)row["user_id"]
					};
					items.Add(item);
					row.Delete();
				}
			}
			return items;
		}

		public static void AddFurni(List<InventoryItem> items, InventoryComponent inventory)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				foreach (InventoryItem item in items)
				{
					dbClient.AddParameter("baseId", item.ItemData.Id);
					dbClient.AddParameter("userId", inventory.Habbo().Id);
					item.Id = (int)dbClient.InsertQuery("INSERT INTO `items` (`base_id`, `user_id`) VALUES (@baseId, @userId)");
					inventory.FloorItems.Add(item);
					dbClient.ClearParameters();
				}
			}
		}

		public static void UpdateFurni(InventoryItem item)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("baseId", item.ItemData.Id);
				dbClient.AddParameter("userId", item.UserId);
				dbClient.AddParameter("roomId", item.RoomId);
				dbClient.AddParameter("itemId", item.Id);
				dbClient.Query("UPDATE `items` SET `base_id` = @baseId, `user_id` = @userId, `room_id` = @roomId WHERE `id` = @itemId");
			}
		}

		public static void RemoveFurni(int itemId)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("itemId", itemId);
				dbClient.Query("DELETE FROM `items` WHERE `id` = @itemId");
			}
		}
	}
}
