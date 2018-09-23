using System.Collections.Generic;
using System.Threading.Tasks;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Players.Crafting
{
    internal class CraftingDao : AbstractDao
    {
		internal async Task<List<int>> ReadPlayerRecipesAsync(int id)
		{
			List<int> recipes = new List<int>();
			await SelectAsync(async reader =>
			{
				while (await reader.ReadAsync())
				{
					int recipeId = reader.ReadData<int>("recipe_id");
					if (!recipes.Contains(recipeId))
					{
						recipes.Add(recipeId);
					}
				}
			}, "SELECT `recipe_id` FROM `habbo_recipes` WHERE `user_id` = @0", id);
			return recipes;
		}

		internal async Task AddPlayerRecipesAsync(int recipeId, int userId)
		{
			await InsertAsync("INSERT INTO `habbo_recipes` (`recipe_id`, `user_id`) VALUES (@0, @1);", recipeId, userId);
		}
	}
}
