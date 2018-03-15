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
				foreach (DataRow row in dbClient.DataTable("SELECT * FROM `room_items` WHERE `room_id` = @roomId").Rows)
				{
					RoomItem item = new RoomItem();
					item.Id = (int)row["id"];
					item.Room = room;
					item.Position = new ItemPosition();
					item.Position.X = (int)row["x"];
					item.Position.Y = (int)row["y"];
					item.Position.Z = (double)row["z"];
					item.Position.Rotation = (int)row["rot"];
					item.Owner = (int)row["user_id"];
					item.ItemData = ItemManager.GetItemData((int)row["base_id"]);
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
				dbClient.AddParameter("roomId", item.Room.Id);
				dbClient.AddParameter("userId", item.Owner);
				dbClient.AddParameter("xPos", item.Position.X);
				dbClient.AddParameter("yPos", item.Position.Y);
				dbClient.AddParameter("ZPos", item.Position.Z);
				dbClient.AddParameter("Rot", item.Position.Rotation);
				dbClient.AddParameter("baseId", item.ItemData.Id);
				dbClient.Query("INSERT INTO `room_items` (`id`, `room_id`, `user_id`, `x`, `y`, `z`, `rot`, `base_id`) VALUES (@itemId, @roomId, @userId, @xPos, @yPos, @zPos, @rot, @baseId)");
			}
		}

		public static void RemoveItem(int itemId)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("itemId", itemId);
				dbClient.Query("DELETE FROM `room_items` WHERE `id` = @itemId");
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
					dbClient.Query("UPDATE `room_items` SET `x` = @x, `y` = @y, `z` = @z, `rot` = @rot WHERE `id` = @id");
					dbClient.ClearParameters();
				}
			}
		}
	}
}
