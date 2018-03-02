using System.Collections.Generic;
using System.Linq;

namespace Alias.Emulator.Hotel.Users.Inventory
{
	public class InventoryComponent
	{
		private List<InventoryItem> floorItems;
		private Habbo habbo;

		public InventoryComponent(Habbo h)
		{
			this.floorItems = InventoryDatabase.ReadFloorItems(h.Id);
			this.habbo = h;
		}

		public void AddItems(List<InventoryItem> items)
		{
			InventoryDatabase.AddFurni(items, habbo.Inventory);
		}

		public void RemoveItem(InventoryItem item)
		{
			InventoryDatabase.RemoveFurni(item.Id);
			floorItems.Remove(item);
		}

		public Habbo Habbo()
		{
			return this.habbo;
		}

		public InventoryItem GetFloorItem(int itemId)
		{
			return this.floorItems.Where(item => item.Id == itemId).FirstOrDefault();
		}

		public List<InventoryItem> FloorItems()
		{
			return this.floorItems;
		}
	}
}