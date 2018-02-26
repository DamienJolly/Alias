using System.Collections.Generic;
using System.Linq;

namespace Alias.Emulator.Hotel.Items
{
	public class ItemManager
	{
		private static List<ItemData> Items;

		public static void Initialize()
		{
			Items = ItemDatabase.ReadItemData();
		}

		public static ItemData GetItemData(int baseId)
		{
			return Items.Where(item => item.Id == baseId).FirstOrDefault();
		}
	}
}
