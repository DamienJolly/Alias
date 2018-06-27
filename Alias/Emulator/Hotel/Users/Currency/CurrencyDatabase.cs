using System.Collections.Generic;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Users.Currency
{
	class CurrencyDatabase
	{
		public static List<CurrencyType> ReadCurrencies(int userId)
		{
			List<CurrencyType> currenies = new List<CurrencyType>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", userId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `type`, `amount` FROM `habbo_currencies` WHERE `user_id` = @id"))
				{
					while (Reader.Read())
					{
						CurrencyType currency = new CurrencyType()
						{
							Type   = Reader.GetInt32("type"),
							Amount = Reader.GetInt32("amount")
						};
						currenies.Add(currency);
					}
				}
			}
			return currenies;
		}

		public static void SaveCurrencies(CurrencyComponent currency)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				foreach (CurrencyType cType in currency.RequestCurrencies())
				{
					dbClient.AddParameter("id", currency.Habbo().Id);
					dbClient.AddParameter("amount", cType.Amount);
					dbClient.AddParameter("type", cType.Type);
					dbClient.Query("UPDATE `habbo_currencies` SET `amount` = @amount WHERE `type` = @type AND `user_id` = @id");
				}
			}
		}
	}
}
