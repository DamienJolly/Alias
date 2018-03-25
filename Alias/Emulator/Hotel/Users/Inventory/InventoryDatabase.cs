using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Users.Inventory
{
	public class InventoryDatabase
	{
		public static List<InventoryItem> ReadFloorItems(int userId)
		{
			List<InventoryItem> items = new List<InventoryItem>();
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
			{
				dbClient.AddParameter("id", userId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `items` WHERE `user_id` = @id AND `room_id` = 0"))
				{
					while (Reader.Read())
					{
						InventoryItem item = new InventoryItem
						{
							Id            = Reader.GetInt32("id"),
							LimitedNumber = Reader.GetInt32("limited_number"),
							LimitedStack  = Reader.GetInt32("limited_stack"),
							ItemData      = Alias.GetServer().GetItemManager().GetItemData(Reader.GetInt32("base_id")),
							UserId        = Reader.GetInt32("user_id")
						};
						items.Add(item);
					}
				}
			}
			return items;
		}

		public static List<InventoryBots> ReadBots(int userId)
		{
			List<InventoryBots> bots = new List<InventoryBots>();
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
			{
				dbClient.AddParameter("id", userId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `bots` WHERE `user_id` = @id AND `room_id` = 0"))
				{
					while (Reader.Read())
					{
						InventoryBots bot = new InventoryBots
						{
							Id     = Reader.GetInt32("id"),
							Name   = Reader.GetString("name"),
							Motto  = Reader.GetString("motto"),
							Look   = Reader.GetString("look"),
							Gender = Reader.GetString("gender")
						};
						bots.Add(bot);
					}
				}
			}
			return bots;
		}

		public static int AddBot(InventoryBots bot, int userId)
		{
			int botId = 0;
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
			{
				dbClient.AddParameter("userId", userId);
				dbClient.AddParameter("name", bot.Name);
				dbClient.AddParameter("motto", bot.Motto);
				dbClient.AddParameter("look", bot.Look);
				dbClient.AddParameter("gender", bot.Gender);
				dbClient.Query("INSERT INTO `bots` (`name`, `motto`, `look`, `gender`, `user_id`) VALUES (@name, @motto, @look, @gender, @userId)");
				botId = dbClient.LastInsertedId();
			}

			return botId;
		}

		public static void AddFurni(List<InventoryItem> items, InventoryComponent inventory)
		{
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
			{
				foreach (InventoryItem item in items)
				{
					dbClient.AddParameter("baseId", item.ItemData.Id);
					dbClient.AddParameter("userId", inventory.Habbo().Id);
					dbClient.Query("INSERT INTO `items` (`base_id`, `user_id`) VALUES (@baseId, @userId)");
					item.Id = dbClient.LastInsertedId();
					inventory.FloorItems.Add(item);
				}
			}
		}

		public static void UpdateFurni(InventoryItem item)
		{
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
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
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
			{
				dbClient.AddParameter("itemId", itemId);
				dbClient.Query("DELETE FROM `items` WHERE `id` = @itemId");
			}
		}
	}
}
