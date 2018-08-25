using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Items.Crafting
{
	class CraftingDatabase
	{
		public static Dictionary<int, CraftingTable> ReadCraftingData()
		{
			Dictionary<int, CraftingTable> data = new Dictionary<int, CraftingTable>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `crafting_tables` ORDER BY `table_id` ASC"))
				{
					while (Reader.Read())
					{
						ItemData item = Alias.Server.ItemManager.GetItemData(Reader.GetInt32("table_id"));
						if (item != null)
						{
							if (!data.ContainsKey(item.Id))
							{
								CraftingTable table = new CraftingTable
								{
									Id = Reader.GetInt32("id")
								};
								table.Recipes = ReadCraftingRecipes(table);
								data.Add(item.Id, table);
							};
						}
					}
				}
			}
			return data;
		}

		public static Dictionary<string, CraftingRecipe> ReadCraftingRecipes(CraftingTable table)
		{
			Dictionary<string, CraftingRecipe> recipes = new Dictionary<string, CraftingRecipe>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", table.Id);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `crafting_recipes` WHERE `table_id` = @id AND `enabled` = 1"))
				{
					while (Reader.Read())
					{
						CraftingRecipe recipe = new CraftingRecipe
						{
							Id = Reader.GetInt32("id"),
							Name = Reader.GetString("prouct_name"),
							Reward = Reader.GetInt32("reward"),
							Secret = Reader.GetBoolean("secret"),
							Ingredients = ReadRecipeIngredients(Reader.GetInt32("id"))
						};
						table.AddIngredients(recipe.Ingredients.Keys.ToList());
						recipes.Add(recipe.Name, recipe);
					}
				}
			}
			return recipes;
		}

		public static Dictionary<int, int> ReadRecipeIngredients(int recipeId)
		{
			Dictionary<int, int> ingredients = new Dictionary<int, int>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", recipeId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `crafting_recipe_ingredients` WHERE `recipe_id` = @id"))
				{
					while (Reader.Read())
					{
						ingredients.Add(Reader.GetInt32("item_id"), Reader.GetInt32("amount"));
					}
				}
			}
			return ingredients;
		}
	}
}
