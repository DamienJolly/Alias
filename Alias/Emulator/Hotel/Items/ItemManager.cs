using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Items.Crafting;

namespace Alias.Emulator.Hotel.Items
{
	sealed class ItemManager
	{
		private List<ItemData> _items;
		private List<CrackableData> _crackableData;

		public CraftingComponent Crafting
		{
			get; set;
		}

		public ItemManager()
		{
			this._items = new List<ItemData>();
			this._crackableData = new List<CrackableData>();
			Crafting = new CraftingComponent();
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
			this.Crafting.Initialize();
		}

		public ItemData GetItemData(int baseId) => this._items.Where(item => item.Id == baseId).FirstOrDefault();

		public CrackableData GetCrackableData(int itemId) => this._crackableData.Where(item => item.ItemId == itemId).FirstOrDefault();
	}
}
