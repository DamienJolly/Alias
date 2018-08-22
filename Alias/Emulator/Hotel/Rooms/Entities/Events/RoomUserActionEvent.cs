using Alias.Emulator.Hotel.Rooms.Entities.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
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

			room.EntityManager.Send(new RoomUserActionComposer(session.Habbo.Entity, action));
		}
	}
}
