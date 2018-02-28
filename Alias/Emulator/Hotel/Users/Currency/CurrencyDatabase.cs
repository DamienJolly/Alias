using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Users.Currency
{
	public class CurrencyDatabase
	{
		public static List<CurrencyType> ReadCurrencies(int userId)
		{
			List<CurrencyType> currenies = new List<CurrencyType>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", userId);
				foreach (DataRow row in dbClient.DataTable("SELECT `type`, `amount` FROM `habbo_currencies` WHERE `user_id` = @id").Rows)
				{
					CurrencyType currency = new CurrencyType()
					{
						Type   = (int)row["type"],
						Amount = (int)row["amount"]
					};
					currenies.Add(currency);
					row.Delete();
				}
			}
			return currenies;
		}

		public static void SaveCurrencies(CurrencyComponent currency)
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
