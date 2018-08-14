using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Trading.Events
{
	class TradeConfirmEvent : IPacketEvent
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

			if (!trade.GetTradeUser(user).Accepted)
			{
				return;
			}

			trade.Confirm(user);
		}
	}
}
