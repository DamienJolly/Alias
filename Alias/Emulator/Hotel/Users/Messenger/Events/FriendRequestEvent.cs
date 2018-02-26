using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger.Events
{
	public class FriendRequestEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			session.Habbo().Messenger().Request(message.String());
		}
	}
}
