using System.Collections.Generic;
using System.Threading.Tasks;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Players.Navigator
{
    internal class NavigatorDao : AbstractDao
	{
		internal async Task<NavigatorSettings> ReadPlayerSettingsAsync(int id)
		{
			NavigatorSettings settings = null;
			await SelectAsync(async reader =>
			{
				while (await reader.ReadAsync())
				{
					settings = new NavigatorSettings(reader);
				}
			}, "SELECT * FROM `navigator_preferences` WHERE `id` = @0", id);
			return settings;
		}

		internal async Task AddPlayerSettingsAsync(int userId)
		{
			await InsertAsync("INSERT INTO `navigator_preferences` (`id`) VALUES (@0);", userId);
		}

		internal async Task UpdatePlayerSettingsAsync(NavigatorSettings settings, int userId)
		{
			await InsertAsync("UPDATE `navigator_preferences` SET `x` = @0, `y` = @1, `width` = @2 `height` = @3, `show_searches` = @4, `unknown_int` = @5 WHERE `id` = @6;", settings.X, settings.Y, settings.Width, settings.Height, settings.ShowSearches, settings.UnknownInt, userId);
		}

		internal async Task<Dictionary<int, NavigatorSearch>> ReadPlayerSearchesAsync(int id)
		{
			Dictionary<int, NavigatorSearch> searches = new Dictionary<int, NavigatorSearch>();
			await SelectAsync(async reader =>
			{
				while (await reader.ReadAsync())
				{
					NavigatorSearch search = new NavigatorSearch(reader);
					if (searches.ContainsKey(search.Id))
					{
						searches.Add(search.Id, search);
					}
				}
			}, "SELECT * FROM `habbo_saved_searches` WHERE `user_id` = @0", id);
			return searches;
		}

		internal async Task AddPlayerSearchAsync(NavigatorSearch search, int userId)
		{
			search.Id = await InsertAsync("INSERT INTO `habbo_saved_searches` (`page`, `code`, `user_id`) VALUES (@0, @1, @2);", search.Page, search.Code, userId);
		}

		internal async Task RemovePlayerSearchAsync(int id)
		{
			await InsertAsync("DELETE FROM `habbo_saved_searches` WHERE `id` = @0;", id);
		}
    }
}
