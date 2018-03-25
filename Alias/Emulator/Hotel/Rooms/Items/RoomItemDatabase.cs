using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Items;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Rooms.Items
{
	public class RoomItemDatabase
	{
		public static List<RoomItem> ReadRoomItems(Room room)
		{
			List<RoomItem> items = new List<RoomItem>();
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
			{
				dbClient.AddParameter("roomId", room.Id);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `items` INNER JOIN `items_room_data` ON `items`.`id` = `items_room_data`.`id` WHERE `items`.`room_id` = @roomId"))
				{
					while (Reader.Read())
					{
						RoomItem item = new RoomItem
						{
							Id            = Reader.GetInt32("id"),
							Room          = room,
							Position      = new ItemPosition
							{
								X         = Reader.GetInt32("x"),
								Y         = Reader.GetInt32("y"),
								Z         = Reader.GetDouble("z"),
								Rotation  = Reader.GetInt32("rot")
							},
							Owner         = Reader.GetInt32("user_id"),
							LimitedNumber = Reader.GetInt32("limited_number"),
							LimitedStack  = Reader.GetInt32("limited_stack"),
							ItemData      = Alias.GetServer().GetItemManager().GetItemData(Reader.GetInt32("base_id"))
						};
						items.Add(item);
					}
				}
			}
			return items;
		}

		public static void AddItem(RoomItem item)
		{
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
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
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
			{
				dbClient.AddParameter("itemId", itemId);
				dbClient.Query("DELETE FROM `items_room_data` WHERE `id` = @itemId");
			}
		}

		public static void SaveFurniture(List<RoomItem> items)
		{
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
			{
				foreach (RoomItem item in items)
				{
					dbClient.AddParameter("x", item.Position.X);
					dbClient.AddParameter("y", item.Position.Y);
					dbClient.AddParameter("z", item.Position.Z);
					dbClient.AddParameter("rot", item.Position.Rotation);
					dbClient.AddParameter("id", item.Id);
					dbClient.Query("UPDATE `items_room_data` SET `x` = @x, `y` = @y, `z` = @z, `rot` = @rot WHERE `id` = @id");
				}
			}
		}
	}
}
