using Alias.Emulator.Hotel.Landing.Composers;
using Alias.Emulator.Hotel.Misc.Composers;
using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Events
{
	class RequestRoomLoadEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int roomId = message.PopInt();
			string password = message.PopString();

			if (RoomLoader.CanEnter(session, roomId, password))
			{
				session.Send(new RoomOpenComposer());
				RoomLoader.Enter(session, Alias.Server.RoomManager.Room(roomId));
			}
			else
			{
				session.Send(new RoomAccessDeniedComposer(""));
				session.Send(new GenericErrorComposer(-100002));
				session.Send(new HotelViewComposer());
			}
		}
	}
}
