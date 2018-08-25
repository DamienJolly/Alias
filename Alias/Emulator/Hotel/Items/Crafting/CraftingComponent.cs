using System.Collections.Generic;

namespace Alias.Emulator.Hotel.Items.Crafting
{
	class CraftingComponent
	{
		private Dictionary<int, CraftingTable> CraftingTables
		{
			get; set;
		}

		public CraftingComponent()
		{
			this.CraftingTables = new Dictionary<int, CraftingTable>();
		}

		public void Initialize()
		{
			if (this.CraftingTables.Count > 0)
			{
				this.CraftingTables.Clear();
			}

			this.CraftingTables = CraftingDatabase.ReadCraftingData();
		}

		public bool TryGetCraftingTable(int id, out CraftingTable table)
		{
			return this.CraftingTables.TryGetValue(id, out table);
		}
	}
}
