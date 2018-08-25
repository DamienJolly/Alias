using System.Collections.Generic;

namespace Alias.Emulator.Hotel.Items.Crafting
{
	class CraftingTable
	{
		public int Id
		{
			get; set;
		}

		public Dictionary<string, CraftingRecipe> Recipes
		{
			get; set;
		}

		public List<int> Ingredients
		{
			get; set;
		} = new List<int>();
		
		public bool TryGetRecipe(string name, out CraftingRecipe recipe)
		{
			return this.Recipes.TryGetValue(name, out recipe);
		}

		public void AddIngredients(List<int> ingredients)
		{
			ingredients.ForEach(ingredient =>
			{
				this.Ingredients.Add(ingredient);
			});
		}
	}
}
