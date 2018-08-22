using Alias.Emulator.Hotel.Rooms.Entities.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
{
	class RoomUserDanceEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			int danceId = message.PopInt();
			if (danceId < 0 || danceId > 5)
			{
				return;
			}

			session.Habbo.Entity.DanceId = danceId;
			room.EntityManager.Send(new RoomUserDanceComposer(session.Habbo.Entity));
		}
	}
}
