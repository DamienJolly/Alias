using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Trading.Events
{
	public class TradeOfferMultipleItemsEvent : IMessageEvent
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

			List<InventoryItem> items = new List<InventoryItem>();
			for (int i = 0; i < message.Integer(); i++)
			{
				InventoryItem item = session.Habbo.Inventory.GetFloorItem(message.Integer());
				if (item != null)
				{
					items.Add(item);
				}
			}

			trade.OfferItems(user, items);
		}
	}
}
