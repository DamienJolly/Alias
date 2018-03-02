using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Users.Inventory;

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

		public static void HandleItems(int logId, TradeUser userOne, TradeUser userTwo)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				foreach (InventoryItem item in userOne.OfferedItems)
				{
					dbClient.AddParameter("id", logId);
					dbClient.AddParameter("userOneId", userOne.User.Habbo.Id);
					dbClient.AddParameter("userTwoId", userTwo.User.Habbo.Id);
					dbClient.AddParameter("itemId", userTwo.User.Habbo.Id);
					dbClient.Query("UPDATE `habbo_inventory` SET `user_id` = @userTwoId WHERE `id` = @itemId");
					dbClient.Query("INSERT INTO `room_trade_log_items` (`id`, `item_id`, `user_id`) VALUES (@id, @itemId, @userOneId)");
					dbClient.ClearParameters();
				}

				foreach (InventoryItem item in userTwo.OfferedItems)
				{
					dbClient.AddParameter("id", logId);
					dbClient.AddParameter("userOneId", userOne.User.Habbo.Id);
					dbClient.AddParameter("userTwoId", userTwo.User.Habbo.Id);
					dbClient.AddParameter("itemId", userTwo.User.Habbo.Id);
					dbClient.Query("UPDATE `habbo_inventory` SET `user_id` = @userOneId WHERE `id` = @itemId");
					dbClient.Query("INSERT INTO `room_trade_log_items` (`id`, `item_id`, `user_id`) VALUES (@id, @itemId, @userTwoId)");
					dbClient.ClearParameters();
				}
			}
		}
	}
}
