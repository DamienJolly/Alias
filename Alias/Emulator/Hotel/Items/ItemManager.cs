using System.Collections.Generic;
using System.Linq;

namespace Alias.Emulator.Hotel.Items
{
	sealed class ItemManager
	{
		private List<ItemData> _items;
		private List<CrackableData> _crackableData;

		public ItemManager()
		{
			_items = new List<ItemData>();
			_crackableData = new List<CrackableData>();
		}

		public void Initialize()
		{
			if (this._items.Count > 0)
			{
				this._items.Clear();
			}
			if (this._crackableData.Count > 0)
			{
				this._crackableData.Clear();
			}

			this._items = ItemDatabase.ReadItemData();
			this._crackableData = ItemDatabase.ReadCrackableData();
		}

		public ItemData GetItemData(int baseId) => this._items.Where(item => item.Id == baseId).FirstOrDefault();

		public CrackableData GetCrackableData(int itemId) => this._crackableData.Where(item => item.ItemId == itemId).FirstOrDefault();
	}
}
