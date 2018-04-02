using Alias.Emulator.Hotel.Rooms.States;
using Alias.Emulator.Hotel.Rooms.Trading.Composers;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Trading.Events
{
	public class TradeStartEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int userId = message.PopInt();

			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			if (userId <= 0 || userId == room.UserManager.UserBySession(session).VirtualId)
			{
				return;
			}

			if (room.RoomData.TradeState == RoomTradeState.FORBIDDEN ||
				(room.RoomData.TradeState == RoomTradeState.OWNER && session.Habbo.Id != room.RoomData.OwnerId))
			{
				session.Send(new TradeStartFailComposer(TradeStartFailComposer.ROOM_TRADING_NOT_ALLOWED));
				return;
			}

			RoomUser userOne = room.UserManager.UserBySession(session);
			if (userOne == null)
			{
				return;
			}

			RoomUser userTwo = room.UserManager.UserByVirtualid(userId);
			if (userTwo == null)
			{
				return;
			}

			if (userOne.Actions.Has("trd"))
			{
				session.Send(new TradeStartFailComposer(TradeStartFailComposer.YOU_ALREADY_TRADING));
			}
			else if (!userOne.Habbo.AllowTrading)
			{
				session.Send(new TradeStartFailComposer(TradeStartFailComposer.YOU_TRADING_OFF));
			}
			else if (userTwo.Actions.Has("trd"))
			{
				session.Send(new TradeStartFailComposer(TradeStartFailComposer.TARGET_ALREADY_TRADING, userTwo.Habbo.Username));
			}
			else if (!userTwo.Habbo.AllowTrading)
			{
				session.Send(new TradeStartFailComposer(TradeStartFailComposer.TARGET_TRADING_OFF, userTwo.Habbo.Username));
			}
			else
			{
				room.RoomTrading.StartTrade(userOne, userTwo);
			}
		}
	}
}
