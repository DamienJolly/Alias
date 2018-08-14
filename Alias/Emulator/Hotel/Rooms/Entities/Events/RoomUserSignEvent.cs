using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
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

			room.EntityManager.UserBySession(session).Actions.Add("sign", signId + "");
		}
	}
}
