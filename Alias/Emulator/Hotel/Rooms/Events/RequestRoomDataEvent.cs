using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Events
{
	class RequestRoomDataEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int roomId = message.PopInt();
			if (!Alias.Server.RoomManager.TryGetRoomData(roomId, out RoomData roomData))
			{
				return;
			}

			int num1 = message.PopInt();
			int num2 = message.PopInt();

			session.Send(new RoomDataComposer(roomData, !(num1 == 0 && num2 == 1), true, session));
		}
	}
}
