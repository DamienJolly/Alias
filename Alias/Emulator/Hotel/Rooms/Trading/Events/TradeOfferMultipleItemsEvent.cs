using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Trading.Events
{
	class TradeOfferMultipleItemsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
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

			int amount = message.PopInt();
			int itemId = message.PopInt();

			InventoryItem item = session.Habbo.Inventory.GetFloorItem(itemId);
			if (item == null)
			{
				return;
			}

			int count = 0;
			List<InventoryItem> items = new List<InventoryItem>();
			foreach (InventoryItem i in session.Habbo.Inventory.FloorItems.Where(x => x.ItemData.Id == item.ItemData.Id))
			{
				if (!trade.GetTradeUser(user).OfferedItems.Contains(i))
				{
					items.Add(i);
					count++;
				}
				if (count == amount)
				{
					break;
				}
			}

			trade.OfferItems(user, items);
		}
	}
}
