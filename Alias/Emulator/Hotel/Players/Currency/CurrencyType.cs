using System.Data.Common;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Players.Currency
{
	public class CurrencyType
	{
		public int Type { get; set; }
		public int Amount { get; set; }

		public CurrencyType(DbDataReader reader)
		{
			Type = reader.ReadData<int>("type");
			Amount = reader.ReadData<int>("amount");
		}
	}
}
