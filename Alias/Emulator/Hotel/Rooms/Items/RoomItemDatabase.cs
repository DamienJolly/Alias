using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Items;

namespace Alias.Emulator.Hotel.Rooms.Items
{
	public class RoomItemDatabase
	{
		public static List<RoomItem> ReadRoomItems(Room room)
		{
			List<RoomItem> items = new List<RoomItem>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("roomId", room.Id);
				foreach (DataRow row in dbClient.DataTable("SELECT * FROM `items` INNER JOIN `items_room_data` ON `items`.`id` = `items_room_data`.`id` WHERE `items`.`room_id` = @roomId").Rows)
				{
					RoomItem item = new RoomItem
					{
						Id = (int)row["id"],
						Room = room,
						Position = new ItemPosition
						{
							X = (int)row["x"],
							Y = (int)row["y"],
							Z = (double)row["z"],
							Rotation = (int)row["rot"]
						},
						Owner = (int)row["user_id"],
						LimitedNumber = (int)row["limited_number"],
						LimitedStack = (int)row["limited_stack"],
						ItemData = ItemManager.GetItemData((int)row["base_id"])
					};
					items.Add(item);
					row.Delete();
				}
			}
			return items;
		}

		public static void AddItem(RoomItem item)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("itemId", item.Id);
				dbClient.AddParameter("xPos", item.Position.X);
				dbClient.AddParameter("yPos", item.Position.Y);
				dbClient.AddParameter("ZPos", item.Position.Z);
				dbClient.AddParameter("Rot", item.Position.Rotation);
				dbClient.Query("INSERT INTO `items_room_data` (`id`, `x`, `y`, `z`, `rot`) VALUES (@itemId, @xPos, @yPos, @zPos, @rot)");
			}
		}

		public static void RemoveItem(int itemId)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("itemId", itemId);
				dbClient.Query("DELETE FROM `items_room_data` WHERE `id` = @itemId");
			}
		}

		public static void SaveFurniture(List<RoomItem> items)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				foreach (RoomItem item in items)
				{
					dbClient.AddParameter("x", item.Position.X);
					dbClient.AddParameter("y", item.Position.Y);
					dbClient.AddParameter("z", item.Position.Z);
					dbClient.AddParameter("rot", item.Position.Rotation);
					dbClient.AddParameter("id", item.Id);
					dbClient.Query("UPDATE `items_room_data` SET `x` = @x, `y` = @y, `z` = @z, `rot` = @rot WHERE `id` = @id");
					dbClient.ClearParameters();
				}
			}
		}
	}
}
