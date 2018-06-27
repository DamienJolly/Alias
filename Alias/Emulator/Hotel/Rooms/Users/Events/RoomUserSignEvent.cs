using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Users.Events
{
	class RoomUserSignEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int signId = message.PopInt();

			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			room.UserManager.UserBySession(session).Actions.Add("sign", signId + "");
		}
	}
}
