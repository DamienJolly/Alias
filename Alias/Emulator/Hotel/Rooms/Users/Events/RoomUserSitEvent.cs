using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Users.Events
{
	public class RoomUserSitEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			Room room = session.Habbo().CurrentRoom;
			if (room == null)
			{
				return;
			}

			room.UserManager.UserBySession(session).MakeSit();
		}
	}
}
