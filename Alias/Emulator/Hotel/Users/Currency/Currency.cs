using System.Collections.Generic;
using System.Linq;

namespace Alias.Emulator.Hotel.Users.Currency
{
	public class Currency
	{
		private List<CurrencyType> currencies;
		private Habbo habbo;

		public Currency(Habbo h)
		{
			this.currencies = new List<CurrencyType>();
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
			CurrencyDatabase.SaveCurrencies(this);
			this.currencies.Clear();
			this.habbo = null;
		}
	}
}
