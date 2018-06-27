using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Events
{
	class RequestRoomDataEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			RoomLoader.PrepareLoading(session, message.PopInt(), message.PopInt(), message.PopInt());
		}
	}
}
