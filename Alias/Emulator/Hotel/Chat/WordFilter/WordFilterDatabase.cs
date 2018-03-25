using System.Collections.Generic;
using Alias.Emulator.Database;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Chat.WordFilter
{
	public class WordFilterDatabase
	{
		public static List<WordFilterData> ReadSwearWords()
		{
			List<WordFilterData> result = new List<WordFilterData>();
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `word`, `bannable` FROM `wordfilter`"))
				{
					while (Reader.Read())
					{
						WordFilterData data = new WordFilterData
						{
							Phrase   = Reader.GetString("Word"),
							Bannable = Reader.GetBoolean("bannable")
						};
						result.Add(data);
					}
				}
			}
			return result;
		}

		public static void StoreMessage(int userId, int roomId, string message, int type, int toId)
		{
			using (DatabaseConnection dbClient = Alias.GetServer().GetDatabase().GetConnection())
			{
				dbClient.AddParameter("userId", userId);
				dbClient.AddParameter("roomId", roomId);
				dbClient.AddParameter("message", (message.Trim(' ').Length > 0) ? message : "*user sent blank message*");
				dbClient.AddParameter("type", type);
				dbClient.AddParameter("toId", toId);
				dbClient.AddParameter("tstamp", (int)Alias.GetUnixTimestamp());
				dbClient.Query("INSERT INTO `chatlogs` (`room_id`, `user_id`, `message`, `type`, `target_id`, `timestamp`) VALUES (@roomId, @userId, @message, @type, @toId, @tstamp)");
			}
		}
	}
}
