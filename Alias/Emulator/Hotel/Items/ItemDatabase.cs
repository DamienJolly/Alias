using System.Collections.Generic;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Rooms.Items;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Items
{
	class ItemDatabase
	{
		public static List<ItemData> ReadItemData()
		{
			List<ItemData> items = new List<ItemData>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `item_data`"))
				{
					while (Reader.Read())
					{
						ItemData item = new ItemData
						{
							Id          = Reader.GetInt32("id"),
							SpriteId    = Reader.GetInt32("sprite_id"),
							Name        = Reader.GetString("item_name"),
							Length      = Reader.GetInt32("length"),
							Width       = Reader.GetInt32("width"),
							Height      = Reader.GetDouble("height"),
							CanSit      = Reader.GetBoolean("can_sit"),
							CanLay      = Reader.GetBoolean("can_lay"),
							ExtraData   = Reader.GetString("extra_data"),
							Type        = Reader.GetString("type"),
							Modes       = Reader.GetInt32("modes"),
							Interaction = ItemInteractions.GetInteractionFromString(Reader.GetString("interaction_type")),
							CanWalk     = Reader.GetBoolean("can_walk"),
							CanStack    = Reader.GetBoolean("can_stack")
						};

						if (item.IsWired && int.TryParse(item.ExtraData, out int wiredId))
						{
							item.WiredInteraction = (WiredInteraction)wiredId;
						}

						items.Add(item);
					}
				}
			}
			return items;
		}

		public static List<CrackableData> ReadCrackableData()
		{
			List<CrackableData> data = new List<CrackableData>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `item_crackable_data`"))
				{
					while (Reader.Read())
					{
						CrackableData item = new CrackableData
						{
							ItemId = Reader.GetInt32("item_id"),
							Tick = Reader.GetInt32("tick")
						};
						item.LoadPrizes(Reader.GetString("prizes"));
						data.Add(item);
					}
				}
			}
			return data;
		}
	}
}
