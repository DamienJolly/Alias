using Alias.Emulator.Database;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Rooms.Trading
{
    class RoomTradingDatabase
    {
		public static int CreateTradeLog(TradeUser userOne, TradeUser userTwo)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("userOneId", userOne.User.Player.Id);
				dbClient.AddParameter("userTwoId", userTwo.User.Player.Id);
				dbClient.AddParameter("timestamp", (int)UnixTimestamp.Now);
				dbClient.Query("INSERT INTO `room_trade_log` (`user_one_id`, `user_two_id`, `timestamp`) VALUES (@userOneId, @userTwoId, @timestamp)");
				return dbClient.LastInsertedId();
			}
		}

		public static void LogTradeItem(int logId, int userId, int itemId)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", logId);
				dbClient.AddParameter("itemId", itemId);
				dbClient.AddParameter("userId", userId);
				dbClient.Query("INSERT INTO `room_trade_log_items` (`id`, `item_id`, `user_id`) VALUES (@id, @itemId, @userId)");
			}
		}
	}
}
