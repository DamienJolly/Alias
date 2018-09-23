using System.Threading.Tasks;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Players
{
	internal class PlayerDao : AbstractDao
	{
		internal async Task<Player> ReadPlayerByIdAsync(int id)
		{
			Player player = null;
			await SelectAsync(async reader =>
			{
				if (await reader.ReadAsync())
				{
					player = new Player(reader);
				}
			}, "SELECT * FROM `habbos` WHERE `id` = @0 LIMIT 1;", id);
			return player;
		}

		internal async Task<Player> ReadPlayerBySSOAsync(string sso)
		{
			Player player = null;
			await SelectAsync(async reader =>
			{
				if (await reader.ReadAsync())
				{
					player = new Player(reader);
				}
			}, "SELECT * FROM `habbos` WHERE `auth_ticket` = @0 LIMIT 1;", sso);
			return player;
		}

		internal async Task<PlayerSettings> ReadPlayerSettingsByIdAsync(int id)
		{
			PlayerSettings settings = null;
			await SelectAsync(async reader =>
			{
				if (await reader.ReadAsync())
				{
					settings = new PlayerSettings(reader);
				}
			}, "SELECT * FROM `habbo_settings` WHERE `id` = @0 LIMIT 1;", id);
			return settings;
		}

		internal async Task AddPlayerSettingsAsync(int id)
		{
			await InsertAsync("INSERT INTO `habbo_settings` (`id`) VALUES(@0)", id);
		}
	}
}
