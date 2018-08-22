using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Trading.Events
{
	class TradeCancelOfferItemEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			RoomTrade trade = room.RoomTrading.GetActiveTrade(session.Habbo.Entity);
			if (trade == null)
			{
				return;
			}

			int itemId = message.PopInt();

			InventoryItem item = session.Habbo.Inventory.GetFloorItem(itemId);
			if (item == null)
			{
				return;
			}

			trade.RemoveItem(session.Habbo.Entity, item);
		}
	}
}
