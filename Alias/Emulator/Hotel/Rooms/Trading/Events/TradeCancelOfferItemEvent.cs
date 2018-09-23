using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Hotel.Players.Inventory;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Trading.Events
{
	class TradeCancelOfferItemEvent : IPacketEvent
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

			int itemId = message.PopInt();

			if(!session.Player.Inventory.TryGetItemById(itemId, out InventoryItem item))
			{
				return;
			}

			trade.RemoveItem(session.Player.Entity, item);
		}
	}
}
