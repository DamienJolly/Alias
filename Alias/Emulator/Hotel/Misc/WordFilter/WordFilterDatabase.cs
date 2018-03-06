using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Misc.WordFilter
{
    public class WordFilterDatabase
    {
		public static List<WordFilterData> ReadSwearWords()
		{
			List<WordFilterData> result = new List<WordFilterData>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				foreach (DataRow row in dbClient.DataTable("SELECT `word`, `bannable` FROM `wordfilter`").Rows)
				{
					WordFilterData data = new WordFilterData();
					data.Phrase = (string)row["Word"];
					data.Bannable = AliasEnvironment.ToBool((string)row["bannable"]);
					result.Add(data);
					row.Delete();
				}
			}
			return result;
		}

		public static void StoreMessage(int userId, int roomId, string message, int type, int toId)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("userId", userId);
				dbClient.AddParameter("roomId", roomId);
				dbClient.AddParameter("message", (message.Trim(' ').Length > 0) ? message : "*user sent blank message*");
				dbClient.AddParameter("type", type);
				dbClient.AddParameter("toId", toId);
				dbClient.AddParameter("tstamp", (int)AliasEnvironment.Time());
				dbClient.Query("INSERT INTO `chatlogs` (`room_id`, `user_id`, `message`, `type`, `target_id`, `timestamp`) VALUES (@roomId, @userId, @message, @type, @toId, @tstamp)");
			}
		}
	}
}
