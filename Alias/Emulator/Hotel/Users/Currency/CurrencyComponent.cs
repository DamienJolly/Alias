using System.Collections.Generic;
using System.Linq;

namespace Alias.Emulator.Hotel.Users.Currency
{
	public class CurrencyComponent
	{
		private List<CurrencyType> currencies;
		private Habbo habbo;

		public CurrencyComponent(Habbo h)
		{
			this.currencies = CurrencyDatabase.ReadCurrencies(h.Id);
			this.habbo = h;
		}

		public Habbo Habbo()
		{
			return this.habbo;
		}

		public List<CurrencyType> RequestCurrencies()
		{
			return this.currencies;
		}
		
		public CurrencyType GetCurrencyType(int currencyId)
		{
			return this.currencies.Where(currency => currency.Type == currencyId).FirstOrDefault();
		}

		public void Dispose()
		{			
			this.currencies.Clear();
			this.habbo = null;
		}
	}
}
