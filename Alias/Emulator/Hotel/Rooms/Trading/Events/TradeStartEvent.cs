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
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			int userId = message.PopInt();
			if (userId <= 0 || userId == session.Habbo.Entity.VirtualId)
			{
				return;
			}

			if (room.RoomData.TradeState == RoomTradeState.FORBIDDEN ||
				(room.RoomData.TradeState == RoomTradeState.OWNER && session.Habbo.Id != room.RoomData.OwnerId))
			{
				session.Send(new TradeStartFailComposer(TradeStartFailComposer.ROOM_TRADING_NOT_ALLOWED));
				return;
			}

			RoomEntity target = room.EntityManager.EntityByVirtualid(userId);
			if (target == null)
			{
				return;
			}

			if (session.Habbo.Entity.Actions.Has("trd"))
			{
				session.Send(new TradeStartFailComposer(TradeStartFailComposer.YOU_ALREADY_TRADING));
			}
			else if (!session.Habbo.Entity.Habbo.AllowTrading)
			{
				session.Send(new TradeStartFailComposer(TradeStartFailComposer.YOU_TRADING_OFF));
			}
			else if (target.Actions.Has("trd"))
			{
				session.Send(new TradeStartFailComposer(TradeStartFailComposer.TARGET_ALREADY_TRADING, target.Habbo.Username));
			}
			else if (!target.Habbo.AllowTrading)
			{
				session.Send(new TradeStartFailComposer(TradeStartFailComposer.TARGET_TRADING_OFF, target.Habbo.Username));
			}
			else
			{
				room.RoomTrading.StartTrade(session.Habbo.Entity, target);
			}
		}
	}
}
