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

		public List<string> Ingredients
		{
			get; set;
		} = new List<string>();
		
		public bool TryGetRecipe(string name, out CraftingRecipe recipe)
		{
			return this.Recipes.TryGetValue(name, out recipe);
		}

		public void AddIngredient(string name)
		{
			if (!this.Ingredients.Contains(name))
			{
				this.Ingredients.Add(name);
			}
		}

		public bool TryGetRecipe(Dictionary<string, int> items, out CraftingRecipe recipe)
		{
			recipe = null;
			foreach (CraftingRecipe tmp in this.Recipes.Values)
			{
				recipe = tmp;
				foreach (KeyValuePair<string, int> ingredient in tmp.Ingredients)
				{
					if (!(items.ContainsKey(ingredient.Key) && items[ingredient.Key] == ingredient.Value))
					{
						recipe = null;
						break;
					}
				}

				if (recipe != null)
				{
					return true;
				}
			}
			return false;
		}

		public Dictionary<CraftingRecipe, bool> GetRecipes(Dictionary<string, int> items)
		{
			Dictionary<CraftingRecipe, bool> foundRecepies = new Dictionary<CraftingRecipe, bool>();
			foreach (CraftingRecipe recipe in this.Recipes.Values)
			{
				bool contains = true;
				bool equals = true;

				equals = items.Count == recipe.Ingredients.Count;

				foreach (KeyValuePair<string, int> item in items)
				{
					if (contains)
					{
						if (recipe.Ingredients.ContainsKey(item.Key))
						{
							if (recipe.Ingredients[item.Key] == item.Value)
							{
								continue;
							}

							equals = false;

							if (recipe.Ingredients[item.Key] > item.Value)
							{
								continue;
							}
						}

						contains = false;
					}
				}

				if (contains)
				{
					foundRecepies.Add(recipe, equals);
				}
			}

			return foundRecepies;
		}
	}
}
