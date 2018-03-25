using System.Collections.Generic;
using System.Linq;

namespace Alias.Emulator.Hotel.Items
{
	sealed class ItemManager
	{
		private List<ItemData> _items;

		public ItemManager()
		{
			_items = new List<ItemData>();
		}

		public void Initialize()
		{
			if (this._items.Count > 0)
			{
				this._items.Clear();
			}

			this._items = ItemDatabase.ReadItemData();
		}

		public ItemData GetItemData(int baseId)
		{
			return this._items.Where(item => item.Id == baseId).FirstOrDefault();
		}
	}
}
