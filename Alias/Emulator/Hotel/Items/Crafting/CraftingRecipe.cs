using System.Collections.Generic;

namespace Alias.Emulator.Hotel.Items.Crafting
{
	class CraftingRecipe
	{
		public int Id
		{
			get; set;
		}

		public ItemData Reward
		{
			get; set;
		}

		public bool Secret
		{
			get; set;
		}

		public Dictionary<string, int> Ingredients
		{
			get; set;
		}
	}
}
