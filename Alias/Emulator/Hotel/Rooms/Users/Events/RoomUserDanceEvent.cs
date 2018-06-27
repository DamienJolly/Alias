using Alias.Emulator.Hotel.Rooms.Users.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Users.Events
{
	class RoomUserDanceEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int danceId = message.PopInt();

			if (danceId < 0 || danceId > 5)
			{
				return;
			}

			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			room.UserManager.Send(new RoomUserDanceComposer(room.UserManager.UserBySession(session), danceId));
		}
	}
}
