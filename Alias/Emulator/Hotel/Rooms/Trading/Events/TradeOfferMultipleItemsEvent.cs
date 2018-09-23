using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Players.Inventory;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Trading.Events
{
	class TradeOfferMultipleItemsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Player.CurrentRoom;
			if (room == null)
			{
				return;
			}

			RoomTrade trade = room.RoomTrading.GetActiveTrade(session.Player.Entity);
			if (trade == null)
			{
				return;
			}

			int amount = message.PopInt();
			int itemId = message.PopInt();
			if(!session.Player.Inventory.TryGetItemById(itemId, out InventoryItem item))
			{
				return;
			}

			int count = 0;
			List<InventoryItem> items = new List<InventoryItem>();
			foreach (InventoryItem i in session.Player.Inventory.Items.Values.Where(x => x.ItemData.Id == item.ItemData.Id))
			{
				if (!trade.GetTradeUser(session.Player.Entity).OfferedItems.Contains(i))
				{
					items.Add(i);
					count++;
				}
				if (count == amount)
				{
					break;
				}
			}

			trade.OfferItems(session.Player.Entity, items);
		}
	}
}
