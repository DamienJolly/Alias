using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alias.Emulator.Hotel.Players.Navigator
{
    internal class NavigatorComponent
    {
		private readonly NavigatorDao _dao;
		private readonly Player _player;

		public NavigatorSettings Settings { get; set; } = null;

		public IDictionary<int, NavigatorSearch> Searches { get; set; }

		internal NavigatorComponent(NavigatorDao dao, Player player)
		{
			_dao = dao;
			_player = player;
			Searches = new Dictionary<int, NavigatorSearch>();
		}

		public async Task Initialize()
		{
			NavigatorSettings settings = await _dao.ReadPlayerSettingsAsync(_player.Id);
			if (settings == null)
			{
				await _dao.AddPlayerSettingsAsync(_player.Id);
				settings = await _dao.ReadPlayerSettingsAsync(_player.Id);
			}
			Settings = settings;

			Searches = await _dao.ReadPlayerSearchesAsync(_player.Id);
		}

		public async Task RemoveSearch(int id)
		{
			if (Searches.ContainsKey(id))
			{
				Searches.Remove(id);
				await _dao.RemovePlayerSearchAsync(id);
			}
		}

		public async Task AddSearch(NavigatorSearch search)
		{
			if (!Searches.ContainsKey(search.Id))
			{
				await _dao.AddPlayerSearchAsync(search, _player.Id);
				Searches.Add(search.Id, search);
			}
		}

		public async Task UpdateSettings() => await _dao.UpdatePlayerSettingsAsync(Settings, _player.Id);
	}
}
