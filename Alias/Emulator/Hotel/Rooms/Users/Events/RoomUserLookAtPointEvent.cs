using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Users.Events
{
	class RoomUserLookAtPointEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			RoomUser user = room.UserManager.UserBySession(session);

			int x = message.PopInt();
			int y = message.PopInt();

			user.LookAtPoint(x, y);
		}
	}
}
