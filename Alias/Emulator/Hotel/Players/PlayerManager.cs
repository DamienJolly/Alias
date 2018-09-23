using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alias.Emulator.Hotel.Players
{
	//todo: add player cache
    internal class PlayerManager
    {
		private readonly PlayerDao _dao;
		private readonly Dictionary<int, Player> _players;

		public PlayerManager(PlayerDao dao)
		{
			_dao = dao;
			_players = new Dictionary<int, Player>();
		}
		
		internal async Task<Player> ReadPlayerByIdAsync(int id)
		{
			if (_players.TryGetValue(id, out Player player))
			{
				return player;
			}

			return await _dao.ReadPlayerByIdAsync(id);
		}

		internal async Task<Player> ReadPlayerBySSOAsync(string sso)
		{
			Player player = await _dao.ReadPlayerBySSOAsync(sso);
			if (!_players.ContainsKey(player.Id))
			{
				_players.Add(player.Id, player);
			}

			return player;
		}

		public bool IsOnline(int userId) => _players.ContainsKey(userId);

		public int OnlinePlayers => _players.Count;

		public void RemovePlayer(int id) => _players.Remove(id);

		internal async Task<PlayerSettings> ReadPlayerSettingsByIdAsync(int id) => await _dao.ReadPlayerSettingsByIdAsync(id);

		internal async Task AddPlayerSettingsAsync(int id) => await _dao.AddPlayerSettingsAsync(id);

		internal async Task UpdatePlayerSettingsAsync(PlayerSettings settings, int id) => await _dao.UpdatePlayerSettingsAsync(settings, id);
	}
}
