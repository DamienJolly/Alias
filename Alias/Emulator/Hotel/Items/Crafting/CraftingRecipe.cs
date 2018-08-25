using System.Collections.Generic;

namespace Alias.Emulator.Hotel.Items.Crafting
{
	class CraftingRecipe
	{
		public int Id
		{
			get; set;
		}

		public string Name
		{
			get; set;
		}

		public int Reward
		{
			get; set;
		}

		public bool Secret
		{
			get; set;
		}

		public Dictionary<int, int> Ingredients
		{
			get; set;
		}
	}
}
