using Alias.Emulator.Hotel.Rooms.Users.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Users.Events
{
	public class RoomUserActionEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			int action = message.Integer();

			Room room = session.Habbo().CurrentRoom;
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
