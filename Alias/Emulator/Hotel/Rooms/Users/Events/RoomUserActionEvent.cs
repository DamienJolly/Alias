using Alias.Emulator.Hotel.Rooms.Users.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Users.Events
{
	class RoomUserActionEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int action = message.PopInt();

			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			if (action == 5)
			{
				//idle
			}
			else
			{
			}

			room.UserManager.Send(new RoomUserActionComposer(room.UserManager.UserBySession(session), action));
		}
	}
}
