using Alias.Emulator.Hotel.Rooms.States;
using Alias.Emulator.Hotel.Rooms.Trading.Composers;
using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Trading.Events
{
	class TradeStartEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Player.CurrentRoom;
			if (room == null)
			{
				return;
			}

			int userId = message.PopInt();
			if (userId <= 0 || userId == session.Player.Entity.VirtualId)
			{
				return;
			}

			if (room.RoomData.TradeState == RoomTradeState.FORBIDDEN ||
				(room.RoomData.TradeState == RoomTradeState.OWNER && session.Player.Id != room.RoomData.OwnerId))
			{
				session.Send(new TradeStartFailComposer(TradeStartFailComposer.ROOM_TRADING_NOT_ALLOWED));
				return;
			}

			RoomEntity target = room.EntityManager.EntityByVirtualid(userId);
			if (target == null)
			{
				return;
			}

			if (session.Player.Entity.Actions.Has("trd"))
			{
				session.Send(new TradeStartFailComposer(TradeStartFailComposer.YOU_ALREADY_TRADING));
			}
			else if (!session.Player.Entity.Player.AllowTrading)
			{
				session.Send(new TradeStartFailComposer(TradeStartFailComposer.YOU_TRADING_OFF));
			}
			else if (target.Actions.Has("trd"))
			{
				session.Send(new TradeStartFailComposer(TradeStartFailComposer.TARGET_ALREADY_TRADING, target.Player.Username));
			}
			else if (!target.Player.AllowTrading)
			{
				session.Send(new TradeStartFailComposer(TradeStartFailComposer.TARGET_TRADING_OFF, target.Player.Username));
			}
			else
			{
				room.RoomTrading.StartTrade(session.Player.Entity, target);
			}
		}
	}
}
