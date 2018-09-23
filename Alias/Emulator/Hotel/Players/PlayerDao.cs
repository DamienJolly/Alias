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
			await InsertAsync("INSERT INTO `habbo_settings` (`id`) VALUES (@0)", id);
		}
		
		internal async Task UpdatePlayerSettingsAsync(PlayerSettings settings, int id)
		{
			await InsertAsync("UPDATE `habbo_settings` SET `volume_system` = @0, `volume_furni` = @1, `volume_trax` = @2, `old_chat` = @3, `ignore_invited` = @4, `camera_follow` = @5 WHERE `id` = @6", settings.VolumeSystem, settings.VolumeFurni, settings.VolumeTrax, settings.OldChat, settings.IgnoreInvites, settings.CameraFollow, id);
		}
	}
}
