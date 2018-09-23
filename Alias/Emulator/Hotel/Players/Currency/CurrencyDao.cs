using System.Collections.Generic;
using System.Threading.Tasks;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Players.Currency
{
    internal class CurrencyDao : AbstractDao
    {
		internal async Task<Dictionary<int, CurrencyType>> ReadCurrenciesAsync(int id)
		{
			Dictionary<int, CurrencyType> currencies = new Dictionary<int, CurrencyType>();
			await SelectAsync(async reader =>
			{
				while (await reader.ReadAsync())
				{
					CurrencyType currency = new CurrencyType(reader);
					if (!currencies.ContainsKey(currency.Type))
					{
						currencies.Add(currency.Type, currency);
					}
				}
			}, "SELECT `type`, `amount` FROM `habbo_currencies` WHERE `user_id` = @0", id);
			return currencies;
		}
	}
}
