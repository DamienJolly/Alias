using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alias.Emulator.Hotel.Players.Crafting
{
    internal class CraftingComponent
    {
		private readonly CraftingDao _dao;
		private readonly Player _player;

		public List<int> Recipes { get; set; }

		internal CraftingComponent(CraftingDao dao, Player player)
		{
			_dao = dao;
			_player = player;
			Recipes = new List<int>();
		}

		public async Task Initialize()
		{
			Recipes = await _dao.ReadPlayerRecipesAsync(_player.Id);
		}

		public async Task AddRecipe(int recipeId)
		{
			if (!Recipes.Contains(recipeId))
			{
				Recipes.Add(recipeId);
				await _dao.AddPlayerRecipesAsync(recipeId, _player.Id);
			}
		}
	}
}
