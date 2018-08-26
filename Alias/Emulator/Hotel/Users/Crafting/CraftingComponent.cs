using System.Collections.Generic;

namespace Alias.Emulator.Hotel.Users.Crafting
{
    class CraftingComponent
    {
		private List<int> craftingRecipes;
		private Habbo habbo;

		public CraftingComponent(Habbo h)
		{
			this.habbo = h;
			this.craftingRecipes = CraftingDatabase.ReadCraftingRecipes(this.habbo.Id);
		}

		public void AddRecipe(int recipeId)
		{
			if (!this.HasRecipe(recipeId))
			{
				this.craftingRecipes.Add(recipeId);
				CraftingDatabase.AddCraftingRecipes(this.habbo.Id, recipeId);
			}
		}

		public void Dispose()
		{
			this.craftingRecipes.Clear();
			this.habbo = null;
		}

		public bool HasRecipe(int recipeId) => this.craftingRecipes.Contains(recipeId);
	}
}
