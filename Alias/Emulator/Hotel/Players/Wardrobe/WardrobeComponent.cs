using System.Collections.Generic;
using System.Threading.Tasks;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Players.Wardrobe
{
    internal class WardrobeComponent
    {
		private static readonly int FIGURE_WAIT_TIME_IN_SEC = 6;

		private readonly WardrobeDao _dao;
		private readonly Player _player;

		private double _figureLastUpdated = 0;
		
		public IDictionary<int, WardrobeItem> WardobeItems { get; set; }

		internal WardrobeComponent(WardrobeDao dao, Player player)
		{
			_dao = dao;
			_player = player;
			WardobeItems = new Dictionary<int, WardrobeItem>();
		}

		public async Task Initialize()
		{
			WardobeItems = await _dao.ReadPlayerWardrobeItemsAsync(_player.Id);
		}

		public async Task AddWardrobeItem(WardrobeItem item)
		{
			if (!WardobeItems.ContainsKey(item.SlotId))
			{
				WardobeItems.Add(item.SlotId, item);
				await _dao.AddPlayerWardrobeItemAsync(item, _player.Id);
			}
		}

		public async Task UpdateWardrobeItem(WardrobeItem item)
		{
			if (WardobeItems.ContainsKey(item.SlotId))
			{
				await _dao.UpdatePlayerWardrobeItemAsync(item, _player.Id);
			}
		}

		public void SetFigureUpdated() => _figureLastUpdated = UnixTimestamp.Now + _figureLastUpdated;

		public int SlotsAvailable => 10; //todo: 10 for vip, 5 for club, 0 for no subscription

		public bool CanChangeFigure => (_figureLastUpdated - UnixTimestamp.Now) > 0 ? false : true;

		public bool TryGetWardrobeItem(int Id, out WardrobeItem Item) => WardobeItems.TryGetValue(Id, out Item);
	}
}
