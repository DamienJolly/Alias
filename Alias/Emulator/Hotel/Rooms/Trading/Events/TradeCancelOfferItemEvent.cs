using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Trading.Events
{
	public class TradeCancelOfferItemEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			RoomUser user = room.UserManager.UserBySession(session);
			if (user == null)
			{
				return;
			}

			RoomTrade trade = room.RoomTrading.GetActiveTrade(user);
			if (trade == null)
			{
				return;
			}

			InventoryItem item = session.Habbo.Inventory.GetFloorItem(message.Integer());
			if (item == null)
			{
				return;
			}

			trade.RemoveItem(user, item);
		}
	}
}