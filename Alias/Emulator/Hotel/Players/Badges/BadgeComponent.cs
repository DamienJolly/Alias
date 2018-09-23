using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alias.Emulator.Hotel.Players.Badges
{
    internal class BadgeComponent
    {
		private readonly BadgeDao _dao;
		private readonly Player _player;

		public IDictionary<string, BadgeDefinition> Badges { get; set; }

		internal BadgeComponent(BadgeDao dao, Player player)
		{
			_dao = dao;
			_player = player;
			Badges = new Dictionary<string, BadgeDefinition>();
		}

		public async Task Initialize()
		{
			Badges = await _dao.ReadPlayerBadgesByIdAsync(_player.Id);
		}

		public async Task ResetSlots()
		{
			foreach (BadgeDefinition badge in GetWearingBadges)
			{
				badge.Slot = 0;
				await _dao.UpdatePlayerBadgeAsync(badge, badge.Code, _player.Id);
			}
		}

		public async Task AddBadgeAsync(string code)
		{
			if (!Badges.ContainsKey(code))
			{
				await _dao.AddPlayerBadgeAsync(code, _player.Id);
				Badges.Add(code, new BadgeDefinition(null) { Code = code, Slot = 0 });
			}
		}

		public async Task RemoveBadgeAsync(string code)
		{
			if (Badges.ContainsKey(code))
			{
				await _dao.RemovePlayerBadgeAsync(code, _player.Id);
				Badges.Remove(code);
			}
		}

		public async Task UpdateBadgeAsync(BadgeDefinition badge, string oldCode)
		{
			if (!Badges.ContainsKey(badge.Code))
			{
				Badges.Remove(oldCode);
				Badges.Add(badge.Code, badge);
				await _dao.UpdatePlayerBadgeAsync(badge, oldCode, _player.Id);
			}
		}

		public void Dispose()
		{
			Badges.Clear();
		}

		public List<BadgeDefinition> GetWearingBadges => Badges.Values.Where(badge => badge.Slot > 0).OrderBy(badge => badge.Slot).ToList();

		public bool TryGetBadge(string code, out BadgeDefinition badge) => Badges.TryGetValue(code, out badge);
	}
}
