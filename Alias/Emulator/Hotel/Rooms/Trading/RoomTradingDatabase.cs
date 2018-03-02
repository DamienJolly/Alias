using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Rooms.Trading
{
    public class RoomTradingDatabase
    {
		public static int CreateTradeLog(TradeUser userOne, TradeUser userTwo)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("userOneId", userOne.User.Habbo.Id);
				dbClient.AddParameter("userTwoId", userTwo.User.Habbo.Id);
				dbClient.AddParameter("timestamp", AliasEnvironment.Time());
				return (int)dbClient.InsertQuery("INSERT INTO `room_trade_log` (`user_one_id`, `user_two_id`, `timestamp`) VALUES (@userOneId, @userTwoId, @timestamp)");
			}
		}

		public static void LogTradeItem(int logId, int userId, int itemId)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", logId);
				dbClient.AddParameter("itemId", itemId);
				dbClient.AddParameter("userId", userId);
				dbClient.Query("INSERT INTO `room_trade_log_items` (`id`, `item_id`, `user_id`) VALUES (@id, @itemId, @userId)");
			}
		}
	}
}
