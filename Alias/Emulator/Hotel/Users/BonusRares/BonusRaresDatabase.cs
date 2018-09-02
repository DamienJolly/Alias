using System.Collections.Generic;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Users.BonusRares
{
	class BonusRaresDatabase
	{
		public static Dictionary<int, int> ReadBonusRares(int userId)
		{
			Dictionary<int, int> bonusRares = new Dictionary<int, int>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", userId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `habbo_bonus_rares` WHERE `user_id` = @id"))
				{
					while (Reader.Read())
					{
						if (!bonusRares.ContainsKey(Reader.GetInt32("bonus_id")))
						{
							bonusRares.Add(Reader.GetInt32("bonus_id"), Reader.GetInt32("progress"));
						}
					}
				}
			}
			return bonusRares;
		}

		public static void AddBonusRare(int userId, int id, int progress)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("bonusId", id);
				dbClient.AddParameter("id", userId);
				dbClient.AddParameter("progress", progress);
				dbClient.Query("INSERT INTO `habbo_bonus_rares` (`bonus_id`, `user_id`, `progress`) VALUES (@bonusId, @userId, @progress)");
			}
		}

		public static void UpdateBonusRare(int userId, int id, int progress)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("bonusId", id);
				dbClient.AddParameter("id", userId);
				dbClient.AddParameter("progress", progress);
				dbClient.Query("UPDATE `habbo_bonus_rares` SET `progress` = @progress WHERE `user_id` = @userId AND `bonus_id` = @bonusId");
			}
		}
	}
}
