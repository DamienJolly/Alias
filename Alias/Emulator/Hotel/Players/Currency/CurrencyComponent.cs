using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alias.Emulator.Hotel.Players.Currency
{
    internal class CurrencyComponent
    {
		private readonly CurrencyDao _dao;
		private readonly Player _player;

		public IDictionary<int, CurrencyType> Currencies { get; set; }

		internal CurrencyComponent(CurrencyDao dao, Player player)
		{
			_dao = dao;
			_player = player;
			Currencies = new Dictionary<int, CurrencyType>();
		}

		public async Task Initialize()
		{
			Currencies = await _dao.ReadCurrenciesAsync(_player.Id);
		}

		public void Dispose()
		{
			// todo: save currencies
		}

		public bool TryGetCurrency(int id, out CurrencyType currency) => Currencies.TryGetValue(id, out currency);
	}
}
