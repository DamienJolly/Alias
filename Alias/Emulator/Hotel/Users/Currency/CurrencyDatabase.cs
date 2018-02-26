using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Users.Currency
{
	public class CurrencyDatabase
	{
		public static void InitCurrency(Currency currency)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", currency.Habbo().Id);
				foreach (DataRow row in dbClient.DataTable("SELECT `type`, `amount` FROM `habbo_currencies` WHERE `user_id` = @id").Rows)
				{
					CurrencyType currencyType = new CurrencyType()
					{
						Type   = (int)row["type"],
						Amount = (int)row["amount"]
					};
					currency.RequestCurrencies().Add(currencyType);
					row.Delete();
				}
			}
		}

		public static void SaveCurrencies(Currency currency)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				foreach (CurrencyType cType in currency.RequestCurrencies())
				{
					dbClient.AddParameter("id", currency.Habbo().Id);
					dbClient.AddParameter("amount", cType.Amount);
					dbClient.AddParameter("type", cType.Type);
					dbClient.Query("UPDATE `habbo_currencies` SET `amount` = @amount WHERE `type` = @type AND `user_id` = @id");
					dbClient.ClearParameters();
				}
			}
		}
	}
}
