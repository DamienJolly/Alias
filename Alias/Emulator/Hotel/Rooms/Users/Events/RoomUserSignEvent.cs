using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Users.Events
{
	public class RoomUserSignEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			int signId = message.Integer();

			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			room.UserManager.UserBySession(session).Actions.Add("sign", signId + "");
		}
	}
}
