using System.Collections.Generic;
using System.Threading.Tasks;
using Alias.Emulator.Hotel.Items;

namespace Alias.Emulator.Hotel.Players.BonusRares
{
    internal class BonusRareComponent
    {
		private readonly BonusRareDao _dao;
		private readonly Player _player;

		public IDictionary<int, int> BonusRares { get; set; }

		internal BonusRareComponent(BonusRareDao dao, Player player)
		{
			_dao = dao;
			_player = player;
			BonusRares = new Dictionary<int, int>();
		}

		public async Task Initialize()
		{
			BonusRares = await _dao.ReadBonusRaresAsync(_player.Id);
		}
		
		public async Task AddBonusRare(int id, int progress)
		{
			if (!BonusRares.ContainsKey(id))
			{
				BonusRares.Add(id, progress);
				await _dao.AddBonusRaresAsync(id, progress, _player.Id);
			}
		}

		public async Task UpdateBonusRare(int id, int progress)
		{
			if (BonusRares.ContainsKey(id))
			{
				BonusRares[id] += progress;
				await _dao.UpdateBonusRaresAsync(id, progress, _player.Id);
			}
		}

		public void GivePrize(int id, ItemData prize)
		{
			//todo:
		}

		public bool TryGetProgress(int id, out int progress) => BonusRares.TryGetValue(id, out progress);
	}
}
