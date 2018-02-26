using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Items;

namespace Alias.Emulator.Hotel.Users.Inventory
{
	public class InventoryDatabase
	{
		public static void InitInventory(Inventory inventory)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", inventory.Habbo().Id);
				foreach (DataRow row in dbClient.DataTable("SELECT * FROM `habbo_inventory` WHERE `user_id` = @id").Rows)
				{
					InventoryItem item = new InventoryItem();
					item.Id = (int)row["id"];
					item.ItemData = ItemManager.GetItemData((int)row["base_id"]);
					inventory.FloorItems().Add(item);
					row.Delete();
				}
			}
		}

		public static void AddFurni(List<InventoryItem> items, Inventory inventory)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				foreach (InventoryItem item in items)
				{
					dbClient.AddParameter("baseId", item.ItemData.Id);
					dbClient.AddParameter("userId", inventory.Habbo().Id);
					item.Id = (int)dbClient.InsertQuery("INSERT INTO `habbo_inventory` (`base_id`, `user_id`) VALUES (@baseId, @userId)");
					inventory.FloorItems().Add(item);
					dbClient.ClearParameters();
				}
			}
		}

		public static void RemoveFurni(int itemId)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("itemId", itemId);
				dbClient.Query("DELETE FROM `habbo_inventory` WHERE `id` = @itemId");
			}
		}
	}
}