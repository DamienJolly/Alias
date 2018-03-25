using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Users.Events
{
	public class RoomUserLookAtPointEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			RoomUser user = room.UserManager.UserBySession(session);

			int x = message.Integer();
			int y = message.Integer();

			user.LookAtPoint(x, y);
		}
	}
}
