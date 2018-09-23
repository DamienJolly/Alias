using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Trading.Events
{
	class TradeAcceptEvent : IPacketEvent
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

			trade.Accept(session.Player.Entity, true);
		}
	}
}
