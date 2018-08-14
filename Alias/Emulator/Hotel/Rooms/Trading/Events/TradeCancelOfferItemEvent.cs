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

			RoomEntity user = room.EntityManager.UserBySession(session);
			if (user == null)
			{
				return;
			}

			RoomTrade trade = room.RoomTrading.GetActiveTrade(user);
			if (trade == null)
			{
				return;
			}

			InventoryItem item = session.Habbo.Inventory.GetFloorItem(message.PopInt());
			if (item == null)
			{
				return;
			}

			trade.RemoveItem(user, item);
		}
	}
}
