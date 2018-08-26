using System.Collections.Generic;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Users.Crafting
{
	internal class CraftingDatabase
	{
		public static List<int> ReadCraftingRecipes(int userId)
		{
			List<int> recipes = new List<int>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", userId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `recipe_id` FROM `habbo_recipes` WHERE `user_id` = @id"))
				{
					while (Reader.Read())
					{
						recipes.Add(Reader.GetInt32("recipe_id"));
					}
				}
			}
			return recipes;
		}

		public static void AddCraftingRecipes(int userId, int recipeId)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", userId);
				dbClient.AddParameter("recipe", recipeId);
				dbClient.DataReader("INSERT INTO `habbo_recipes` (`recipe_id`, `user_id`) VALUES (@recipe, @id)");
			}
		}
	}
}
