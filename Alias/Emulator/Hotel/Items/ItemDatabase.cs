using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Rooms.Items;

namespace Alias.Emulator.Hotel.Items
{
	public class ItemDatabase
	{
		public static List<ItemData> ReadItemData()
		{
			List<ItemData> items = new List<ItemData>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				foreach (DataRow row in dbClient.DataTable("SELECT * FROM `item_data`").Rows)
				{
					ItemData item = new ItemData
					{
						Id = (int)row["id"],
						SpriteId = (int)row["sprite_id"],
						Length = (int)row["length"],
						Width = (int)row["width"],
						Height = (double)row["height"],
						CanSit = AliasEnvironment.ToBool((string)row["can_sit"]),
						CanLay = AliasEnvironment.ToBool((string)row["can_lay"]),
						ExtraData = (string)row["extra_data"],
						Type = (string)row["type"],
						Interaction = ItemInteractions.GetInteractionFromString((string)row["interaction_type"]),
						CanWalk = AliasEnvironment.ToBool((string)row["can_walk"])
					};

					//todo: recode
					if (item.IsWired())
					{
						item.WiredInteraction = WiredInteraction.DEFAULT;
						//item.WiredInteraction = (WiredInteraction)item.BehaviourData;
					}

					items.Add(item);
					row.Delete();
				}
			}
			return items;
		}
	}
}
