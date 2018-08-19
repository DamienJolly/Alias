using System.Collections.Generic;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Users.Inventory
{
	class InventoryDatabase
	{
		public static List<InventoryItem> ReadFloorItems(int userId)
		{
			List<InventoryItem> items = new List<InventoryItem>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
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
							ItemData      = Alias.Server.ItemManager.GetItemData(Reader.GetInt32("base_id")),
							UserId        = Reader.GetInt32("user_id"),
							ExtraData     = Reader.GetString("extradata"),
							Mode          = Reader.GetInt32("mode")
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
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", userId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `bots` WHERE `user_id` = @id AND `room_id` = 0"))
				{
					while (Reader.Read())
					{
						InventoryBots bot = new InventoryBots
						{
							Id       = Reader.GetInt32("id"),
							Name     = Reader.GetString("name"),
							Motto    = Reader.GetString("motto"),
							Look     = Reader.GetString("look"),
							Gender   = Reader.GetString("gender"),
							DanceId  = Reader.GetInt32("dance_id"),
							EffectId = Reader.GetInt32("effect_id"),
							CanWalk  = Reader.GetBoolean("can_walk")
						};
						bots.Add(bot);
					}
				}
			}
			return bots;
		}

		public static List<InventoryPets> ReadPets(int userId)
		{
			List<InventoryPets> pets = new List<InventoryPets>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", userId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `pets` WHERE `user_id` = @id AND `room_id` = 0"))
				{
					while (Reader.Read())
					{
						InventoryPets pet = new InventoryPets
						{
							Id     = Reader.GetInt32("id"),
							Name   = Reader.GetString("name"),
							Type   = Reader.GetInt32("type"),
							Race   = Reader.GetInt32("race"),
							Colour = Reader.GetString("colour")
						};
						pets.Add(pet);
					}
				}
			}
			return pets;
		}

		public static int AddBot(InventoryBots bot, int userId)
		{
			int botId = 0;
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
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

		public static void UpdateBot(InventoryBots bot)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("botId", bot.Id);
				dbClient.AddParameter("roomId", bot.RoomId);
				dbClient.Query("UPDATE `bots` SET `room_id` = @roomId WHERE `id` = @botId");
			}
		}

		public static void AddFurni(InventoryItem item)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("baseId", item.ItemData.Id);
				dbClient.AddParameter("userId", item.UserId);
				dbClient.AddParameter("limitedStack", item.LimitedStack);
				dbClient.AddParameter("limitedNum", item.LimitedNumber);
				dbClient.AddParameter("extradata", item.ExtraData);
				dbClient.Query("INSERT INTO `items` (`base_id`, `user_id`, `limited_stack`, `limited_number`, `extradata`) VALUES (@baseId, @userId, @limitedStack, @limitedNum, @extradata)");
				item.Id = dbClient.LastInsertedId();
			}
		}

		public static void UpdateFurni(InventoryItem item)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("baseId", item.ItemData.Id);
				dbClient.AddParameter("userId", item.UserId);
				dbClient.AddParameter("extradata", item.ExtraData);
				dbClient.AddParameter("roomId", item.RoomId);
				dbClient.AddParameter("itemId", item.Id);
				dbClient.Query("UPDATE `items` SET `base_id` = @baseId, `extradata` = @extradata, `user_id` = @userId, `room_id` = @roomId WHERE `id` = @itemId");
			}
		}

		public static void RemoveFurni(int itemId)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("itemId", itemId);
				dbClient.Query("DELETE FROM `items` WHERE `id` = @itemId");
			}
		}
	}
}
