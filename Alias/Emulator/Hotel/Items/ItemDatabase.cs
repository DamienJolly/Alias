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
					ItemData item = new ItemData();
					item.Id = (int)row["id"];
					item.Length = (int)row["length"];
					item.Width = (int)row["width"];
					item.Height = (double)row["height"];
					item.CanSit = AliasEnvironment.ToBool((string)row["can_sit"]);
					item.CanLay = AliasEnvironment.ToBool((string)row["can_lay"]);
					item.Interaction = ItemInteractions.GetInteractionFromString((string)row["type"]);
					item.CanWalk = AliasEnvironment.ToBool((string)row["can_walk"]);
					items.Add(item);
					row.Delete();
				}
			}
			return items;
		}
	}
}
